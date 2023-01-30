using System.Net;
using System.Net.Mail;
using DentOnline.Application.Constants;
using DentOnline.Application.Features.Notifications.Abstracts;
using DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentById;
using DentOnline.Application.Features.Users._Bases.Queries.GetUserById;
using DentOnline.Application.Features.Users.OtherFeatures.Admins.Queries.GetAdminEmailAddresses;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Application.Utilities.MultiLanguage.Services;
using DentOnline.Application.Utilities.Responses.Abstracts;
using DentOnline.Application.Utilities.Responses.Concretes;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;
using DentOnline.Infrastructure.Options;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DentOnline.Infrastructure.Services.Notifications.Emails;

public class EmailNotificationService : INotificationService
{
    private readonly IConfiguration _configuration;
    private readonly EmailSettingOption _emailSettingOption;
    private readonly bool _isServiceActive = true;
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;

    public EmailNotificationService(IMediator mediator, LanguageService languageService,
        IOptions<EmailSettingOption> emailSettingOption, IConfiguration configuration)
    {
        _mediator = mediator;
        _languageService = languageService;
        _configuration = configuration;
        _emailSettingOption = emailSettingOption.Value;
    }

    public async Task<IResponse> SendNotificationWhenCreatedATreatmentAsync(string treatmentId)
    {
        if (_isServiceActive is false)
            return new SuccessResponse(_languageService.Get(Messages.EmailNotificationServiceIsDeactive));

        var treatmentResult = await _mediator.Send(new GetTreatmentByIdQueryRequest(treatmentId));
        if (treatmentResult.IsSuccess is false)
            return new SuccessResponse(treatmentResult.Message);

        var userIdOfDoctor = treatmentResult.Data.Doctor?.Id ??
                             throw new ErrorException(_languageService.Get(Messages.DoctorIsNotFound));

        var userOfDoctorResult = await _mediator.Send(new GetUserByIdQueryRequest(userIdOfDoctor));
        if (userOfDoctorResult.IsSuccess is false)
            return new ErrorResponse(userOfDoctorResult.Message);

        var adminEmailAddressesResult = await _mediator.Send(new GetAdminEmailAddressesQueryRequest());
        if (adminEmailAddressesResult.IsSuccess is false)
            return new ErrorResponse(adminEmailAddressesResult.Message);

        var rawContent = _languageService.Get(Messages.SendNotificationWhenCreatedATreatmentTemplate);

        var content = string.Format(rawContent, userOfDoctorResult.Data.FullName, treatmentResult.Data.CreatedDate);

        return await SendAsync(adminEmailAddressesResult.Data,
            _languageService.Get(Messages.NewTreatmentIsCreatedAsHeader), content);
    }

    public async Task<IResponse> SendNotificationWhenAddedACommentToTreatmentAsync(string treatmentId)
    {
        if (_isServiceActive is false)
            return new SuccessResponse(_languageService.Get(Messages.EmailNotificationServiceIsDeactive));

        var treatmentResult = await _mediator.Send(new GetTreatmentByIdQueryRequest(treatmentId));
        if (treatmentResult.IsSuccess is false)
            return new SuccessResponse(treatmentResult.Message);

        var lastComment = treatmentResult.Data.Comments.LastOrDefault();
        if (lastComment is null)
            return new ErrorResponse(_languageService.Get(Messages.LastCommentIsNotFound));

        var destinationEmailAddresses = new HashSet<string>();

        string content = string.Empty, subject = string.Empty;

        if (lastComment.User.Role.IsDoctor) // Admin'e yeni mesaj gönderildiğine dair bildirim atılır.
        {
            var lastCommentOwnerAdmin = treatmentResult.Data.Comments
                .OrderByDescending(_comment => _comment.CreatedDate)
                .FirstOrDefault(_comment => _comment.User.Role.IsAdmin);

            if (lastCommentOwnerAdmin is null) // Tüm adminlere bildirim atılır.
            {
                var adminEmailAddressesResult = await _mediator.Send(new GetAdminEmailAddressesQueryRequest());
                if (adminEmailAddressesResult.IsSuccess is false)
                    return new ErrorResponse(adminEmailAddressesResult.Message);

                destinationEmailAddresses = adminEmailAddressesResult.Data;
            }
            else // Son yorum gönderen admine bildirim atılır.
            {
                destinationEmailAddresses.Add(lastCommentOwnerAdmin.User.Email);
            }

            subject = _languageService.Get(Messages.DoctorAddedNewCommentToTreatmentAsHeader);

            var template = _languageService.Get(Messages.DoctorAddedNewCommentToTreatmentTemplate);

            content = string.Format(template, treatmentResult.Data.Doctor.FullName, treatmentId);
        }
        else if (lastComment.User.Role.IsAdmin) // Admin yeni mesaj gönderdiğinde Doktora bildirim gönderilir.
        {
            subject = _languageService.Get(Messages.AdminAddedNewCommentToTreatmentAsHeader);

            var template = _languageService.Get(Messages.AdminAddedNewCommentToTreatmentTemplate);

            content = string.Format(template, lastComment.User.FullName, treatmentId);

            destinationEmailAddresses.Add(treatmentResult.Data.Doctor.Email);
        }
        else
        {
            return new ErrorResponse(_languageService.Get(Messages.LastCommentOwnerRoleIsNotFound));
        }

        return await SendAsync(destinationEmailAddresses, subject, content);
    }

    public async Task<IResponse> SendEmailActivationNotificationToDoctorAsync(string userId)
    {
        var webUrl = _configuration.GetSection("ClientUrls:WebUrl")?.Value ??
                     throw new ErrorException(_languageService.Get(Messages.WebClientUrlIsNotFound));

        var userResult = await _mediator.Send(new GetUserByIdQueryRequest(userId));
        if (userResult.IsSuccess is false)
            return new ErrorResponse(userResult.Message);

        var userVerification = userResult.Data.UserVerifications.LastOrDefault(_userVerification =>
            _userVerification.VerificationType.Equals(UserVerificationType.Email) &&
            _userVerification.ExpiryDate > DateTime.UtcNow && _userVerification.IsUsed is false);
        if (userVerification is null)
            return new ErrorResponse(_languageService.Get(Messages.ActiveUserVerificationIsNotFound));

        webUrl += $"email-verify/{userVerification.Code}";

        var template = string.Format(_languageService.Get(Messages.EmailVerificationTemplate), webUrl);

        await SendAsync(new HashSet<string> { userResult.Data.Email },
            _languageService.Get(Messages.EmailVerificationHeader), template);

        return new SuccessResponse(_languageService.Get(Messages.EmailActivationEmailIsSendedToUser));
    }

    public async Task<IResponse> SendAsync(HashSet<string> emailAddresses, string subject, string content)
    {
        SmtpClient smtpClient = new()
        {
            Port = _emailSettingOption.Port,
            Host = _emailSettingOption.Host,
            EnableSsl = _emailSettingOption.EnableSsl,
            Credentials = new NetworkCredential(_emailSettingOption.Email, _emailSettingOption.Password)
        };

        MailMessage mailMessage = new()
        {
            From = new MailAddress(_emailSettingOption.Email, _emailSettingOption.DisplayName),
            Subject = subject,
            Body = content
        };

        foreach (var emailAddress in emailAddresses)
            mailMessage.To.Add(emailAddress);

        await smtpClient.SendMailAsync(mailMessage);

        return new SuccessResponse(_languageService.Get(Messages.EmailIsSended));
    }
}