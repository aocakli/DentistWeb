using DentOnline.Application.Constants;
using DentOnline.Application.Features.Users._Bases.BusinessRules;
using DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.GenerateUserVerification;
using DentOnline.Application.Helpers;
using DentOnline.Application.Repositories.Users;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Domain.Concrete.Users._Bases;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;

namespace DentOnline.Application.Features.Users._Bases.Commands.CreateUser;

public class CreateUserCommandRequestHandler : IRequestHandler<CreateUserCommandRequest, IDataResponse<User>>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly UserBusinessRules _userBusinessRules;
    private readonly IUserWriteRepository _writeRepository;

    public CreateUserCommandRequestHandler(IUserWriteRepository writeRepository, IMapper mapper,
        UserBusinessRules userBusinessRules, LanguageService languageService, IMediator mediator)
    {
        _writeRepository = writeRepository;
        _mapper = mapper;
        _userBusinessRules = userBusinessRules;
        _languageService = languageService;
        _mediator = mediator;
    }

    public async Task<IDataResponse<User>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        await _userBusinessRules.EmailShouldNotExistInDatabaseAsync(request.Email);

        var documentToAdd = _mapper.Map<CreateUserCommandRequest, User>(request);

        documentToAdd.Password = HashingHelper.HashPassword(documentToAdd.Password);

        var generatedUserVerificationResult =
            await _mediator.Send(new GenerateUserVerificationCommandRequest(UserVerificationType.Email));
        if (generatedUserVerificationResult.IsSuccess is false)
            return new ErrorDataResponse<User>(generatedUserVerificationResult.Message);

        (documentToAdd.UserVerifications ??= new List<UserVerification>()).Add(generatedUserVerificationResult.Data);

        await _writeRepository.CreateAsync(documentToAdd);

        if (request.IsSaveChanges && await _writeRepository.SaveChangesAsync() is false)
            throw new ErrorException(_languageService.Get(Messages.UserIsNotCreated));

        return new SuccessDataResponse<User>(_languageService.Get(Messages.UserIsCreated), documentToAdd);
    }
}