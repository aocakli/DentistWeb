using DentOnline.Application.Constants;
using DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.CreateUserVerification;
using DentOnline.Application.Repositories.Users;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Queries.CheckAndVerifyUserVerification;

public class
    CheckAndVerifyUserVerificationQueryRequestHandler : IRequestHandler<CheckAndVerifyUserVerificationQueryRequest,
        IResponse>
{
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;

    public CheckAndVerifyUserVerificationQueryRequestHandler(IUserReadRepository userReadRepository,
        IUserWriteRepository userWriteRepository, LanguageService languageService, IMediator mediator)
    {
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _languageService = languageService;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(CheckAndVerifyUserVerificationQueryRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetAsync(_user =>
            _user.UserVerifications.Any(_userVerification =>
                _userVerification.Code.Equals(request.Code) && _userVerification.IsUsed == false));
        if (user is null)
            return new ErrorResponse(_languageService.Get(Messages.CodeIsInvalid));

        var userVerification =
            user.UserVerifications.First(_userVerification => _userVerification.Code.Equals(request.Code));

        if (userVerification.ExpiryDate < DateTime.UtcNow)
        {
            await _mediator.Send(new CreateUserVerificationCommandRequest(user.Id, UserVerificationType.Email, true));

            throw new BusinessException(_languageService.Get(Messages.ThisCodeIsExpired,
                Messages.WeSendAVerificationNotificationToYou));
        }

        userVerification.IsUsed = true;

        await _userWriteRepository.UpdateAsync(user.Id, user);

        await _userWriteRepository.SaveChangesAsync();

        return new SuccessResponse(_languageService.Get(Messages.AccountVerificationOperationIsCompleted));
    }
}