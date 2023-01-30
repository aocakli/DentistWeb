using DentOnline.Application.Constants;
using DentOnline.Application.Features.Notifications.Abstracts;
using DentOnline.Application.Features.Users._Bases.Commands.CreateUser;

namespace DentOnline.Application.Features.Users.OtherFeatures.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandRequestHandler : IRequestHandler<CreateDoctorCommandRequest, IResponse>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationService _notificationService;

    public CreateDoctorCommandRequestHandler(IMediator mediator, IMapper mapper, LanguageService languageService,
        INotificationService notificationService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _languageService = languageService;
        _notificationService = notificationService;
    }

    public async Task<IResponse> Handle(CreateDoctorCommandRequest request, CancellationToken cancellationToken)
    {
        var createUserCommandRequest = _mapper.Map<CreateDoctorCommandRequest, CreateUserCommandRequest>(request);

        var createdUserResult = await _mediator.Send(createUserCommandRequest);
        if (createdUserResult.IsSuccess is false)
            return new ErrorResponse(createdUserResult.Message);

        request.CreateClinicInformationCommandRequest.UserId = createdUserResult.Data.Id;

        request.CreateClinicInformationCommandRequest.IsSaveChanges = true;

        var createdClinicInformationResult = await _mediator.Send(request.CreateClinicInformationCommandRequest);
        if (createdClinicInformationResult.IsSuccess is false)
            return new ErrorResponse(createdClinicInformationResult.Message);

        await _notificationService.SendEmailActivationNotificationToDoctorAsync(createdUserResult.Data.Id);

        return new SuccessResponse(_languageService.Get(Messages.DoctorIsCreated));
    }
}