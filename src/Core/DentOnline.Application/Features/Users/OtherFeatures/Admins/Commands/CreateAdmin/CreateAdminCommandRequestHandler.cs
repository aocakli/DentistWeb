using DentOnline.Application.Constants;
using DentOnline.Application.Features.Users._Bases.Commands.CreateUser;

namespace DentOnline.Application.Features.Users.OtherFeatures.Admins.Commands.CreateAdmin;

public class CreateAdminCommandRequestHandler : IRequestHandler<CreateAdminCommandRequest, IResponse>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateAdminCommandRequestHandler(IMediator mediator, IMapper mapper, LanguageService languageService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _languageService = languageService;
    }

    public async Task<IResponse> Handle(CreateAdminCommandRequest request, CancellationToken cancellationToken)
    {
        var createUserCommandRequest = _mapper.Map<CreateAdminCommandRequest, CreateUserCommandRequest>(request);

        createUserCommandRequest.IsSaveChanges = true;

        var createdUserResult = await _mediator.Send(createUserCommandRequest);
        if (createdUserResult.IsSuccess is false)
            return new ErrorResponse(createdUserResult.Message);

        return new SuccessResponse(_languageService.Get(Messages.AdminIsCreated));
    }
}