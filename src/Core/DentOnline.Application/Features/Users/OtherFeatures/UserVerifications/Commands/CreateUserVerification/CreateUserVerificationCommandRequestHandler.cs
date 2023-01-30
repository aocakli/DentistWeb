using DentOnline.Application.Constants;
using DentOnline.Application.Features.Notifications.Abstracts;
using DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.GenerateUserVerification;
using DentOnline.Application.Repositories.Users;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.CreateUserVerification;

public class
    CreateUserVerificationCommandRequestHandler : IRequestHandler<CreateUserVerificationCommandRequest, IResponse>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationService _notificationService;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;

    public CreateUserVerificationCommandRequestHandler(IMediator mediator, IMapper mapper,
        IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository,
        LanguageService languageService, INotificationService notificationService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _languageService = languageService;
        _notificationService = notificationService;
    }

    public async Task<IResponse> Handle(CreateUserVerificationCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetAsync(_user => request.UserId.Equals(_user.Id));
        if (user is null)
            return new ErrorResponse(_languageService.Get(Messages.UserIsNotFound));

        var generatedUserVerificationResult =
            await _mediator.Send(new GenerateUserVerificationCommandRequest(request.VerificationType));
        if (generatedUserVerificationResult.IsSuccess is false)
            return new ErrorResponse(generatedUserVerificationResult.Message);

        // Remove old codes.
        user.UserVerifications = user.UserVerifications?
            .Where(_userVerification => _userVerification.VerificationType.Equals(request.VerificationType) == false)?
            .ToList();

        (user.UserVerifications ??= new List<UserVerification>()).Add(generatedUserVerificationResult.Data);

        await _userWriteRepository.UpdateAsync(request.UserId, user);

        await _userWriteRepository.SaveChangesAsync();

        await _notificationService.SendEmailActivationNotificationToDoctorAsync(user.Id);

        return new SuccessResponse(_languageService.Get(Messages.UserVerificationIsAdded));
    }
}