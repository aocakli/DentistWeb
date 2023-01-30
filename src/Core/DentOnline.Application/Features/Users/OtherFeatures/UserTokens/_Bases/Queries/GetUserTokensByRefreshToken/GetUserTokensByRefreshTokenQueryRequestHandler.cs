using DentOnline.Application.Constants;
using DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.AccessTokens.Queries.
    GenerateUserAccessToken;
using DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.RefreshTokens.Commands.
    CreateUserRefreshToken;
using DentOnline.Application.Repositories.Users;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Queries.GetUserTokensByRefreshToken;

public class
    GetUserTokensByRefreshTokenQueryRequestHandler : IRequestHandler<GetUserTokensByRefreshTokenQueryRequest,
        IDataResponse<GetUserTokensByRefreshTokenQueryResponse>>
{
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;
    private readonly IUserReadRepository _userReadRepository;

    public GetUserTokensByRefreshTokenQueryRequestHandler(IMediator mediator, IUserReadRepository userReadRepository,
        LanguageService languageService)
    {
        _mediator = mediator;
        _userReadRepository = userReadRepository;
        _languageService = languageService;
    }

    public async Task<IDataResponse<GetUserTokensByRefreshTokenQueryResponse>> Handle(
        GetUserTokensByRefreshTokenQueryRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetAsync(_user =>
            _user.Id.Equals(request.UserId) && _user.RefreshToken.Token.Equals(request.RefreshToken));
        if (user is null)
            return new ErrorDataResponse<GetUserTokensByRefreshTokenQueryResponse>(
                _languageService.Get(Messages.UserIsNotFound));

        if (DateTime.UtcNow > user.RefreshToken.ExpiryDate)
            return new ErrorDataResponse<GetUserTokensByRefreshTokenQueryResponse>(
                _languageService.Get(Messages.RefreshTokenIsExpired));

        var generatedAccessTokenResult = await _mediator.Send(new GenerateUserAccessTokenQueryRequest(request.UserId));
        if (generatedAccessTokenResult.IsSuccess is false)
            return new ErrorDataResponse<GetUserTokensByRefreshTokenQueryResponse>(generatedAccessTokenResult.Message);

        var createdRefreshTokenResult = await _mediator.Send(new CreateUserRefreshTokenCommandRequest(user));
        if (createdRefreshTokenResult.IsSuccess is false)
            return new ErrorDataResponse<GetUserTokensByRefreshTokenQueryResponse>(createdRefreshTokenResult.Message);

        GetUserTokensByRefreshTokenQueryResponse responseModel =
            new(generatedAccessTokenResult.Data, createdRefreshTokenResult.Data);

        return new SuccessDataResponse<GetUserTokensByRefreshTokenQueryResponse>(
            _languageService.Get(Messages.TokensAreGenerated),
            responseModel);
    }
}