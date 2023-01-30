using DentOnline.Application.Constants;
using DentOnline.Application.Features.Notifications.Abstracts;
using DentOnline.Application.Features.Users._Bases.Dtos;
using DentOnline.Application.Features.Users._Bases.Queries.GetUserById;
using DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.AccessTokens.Queries.
    GenerateUserAccessToken;
using DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.RefreshTokens.Commands.
    CreateUserRefreshToken;
using DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.CreateUserVerification;
using DentOnline.Application.Helpers;
using DentOnline.Application.Repositories.Users;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Domain.Concrete.Users._Bases;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;

namespace DentOnline.Application.Features.Users._Bases.Queries.LoginUser;

public class
    LoginUserQueryRequestHandler : IRequestHandler<LoginUserQueryRequest, IDataResponse<LoginUserQueryResponse>>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationService _notificationService;
    private readonly IUserReadRepository _readRepository;

    public LoginUserQueryRequestHandler(IUserReadRepository readRepository, IMapper mapper, IMediator mediator,
        LanguageService languageService, INotificationService notificationService)
    {
        _readRepository = readRepository;
        _mapper = mapper;
        _mediator = mediator;
        _languageService = languageService;
        _notificationService = notificationService;
    }

    public async Task<IDataResponse<LoginUserQueryResponse>> Handle(LoginUserQueryRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _readRepository.GetAsync(_user => request.Email.Equals(_user.Email));
        if (user is null)
            return new ErrorDataResponse<LoginUserQueryResponse>(
                _languageService.Get(Messages.EmailOrPasswordIncorrect));

        if (HashingHelper.VerifyPassword(request.Password, user.Password) is false)
            return new ErrorDataResponse<LoginUserQueryResponse>(
                _languageService.Get(Messages.EmailOrPasswordIncorrect));

        await CheckUserVerificationAndSendNewVerificationNotificationAsync(user);

        var userResult = await _mediator.Send(new GetUserByIdQueryRequest(user.Id));
        if (userResult.IsSuccess is false)
            return new ErrorDataResponse<LoginUserQueryResponse>(userResult.Message);

        var loginDto = _mapper.Map<UserDto, LoginUserQueryResponse>(userResult.Data);

        await GenerateUserAccessTokenAndMapAsync(user.Id, loginDto);

        /* Bu adıma kadar user güncellenirse aşağıda ki method eski user verisini kullanacağı için veri uyuşmazlığı olacaktır.
         Bu adıma kadar user güncellendiyse aşağıda ki methodu userId ile çalışacak şekilde düzeltin. */
        await CreateUserRefreshTokenAndMapAsync(user, loginDto);

        return new SuccessDataResponse<LoginUserQueryResponse>(_languageService.Get(Messages.LoginOperationIsSuccess),
            loginDto);
    }

    private async Task CheckUserVerificationAndSendNewVerificationNotificationAsync(User user)
    {
        if (
            user.UserVerifications is null || user.UserVerifications.Any() is false ||
            (user.Roles.Contains(UserRoles.Doctor) &&
             user.UserVerifications.Any(_userVerification => _userVerification.IsUsed) is false))
        {
            await _mediator.Send(new CreateUserVerificationCommandRequest(user.Id, UserVerificationType.Email, true));

            throw new BusinessException(_languageService.Get(Messages.PleaseVerifyYourAccount,
                Messages.WeSendAVerificationNotificationToYou));
        }
    }

    private async Task GenerateUserAccessTokenAndMapAsync(string userId, LoginUserQueryResponse loginDto)
    {
        var generatedUserAccessTokenResult = await _mediator.Send(new GenerateUserAccessTokenQueryRequest(userId));
        if (generatedUserAccessTokenResult.IsSuccess is false)
            throw new ErrorException(generatedUserAccessTokenResult.Message);

        loginDto.AccessToken = generatedUserAccessTokenResult.Data;
    }

    private async Task CreateUserRefreshTokenAndMapAsync(User user, LoginUserQueryResponse loginDto)
    {
        var createdUserRefreshTokenResult =
            await _mediator.Send(new CreateUserRefreshTokenCommandRequest(user));
        if (createdUserRefreshTokenResult.IsSuccess is false)
            throw new ErrorException(createdUserRefreshTokenResult.Message);

        loginDto.RefreshToken = createdUserRefreshTokenResult.Data;
    }
}