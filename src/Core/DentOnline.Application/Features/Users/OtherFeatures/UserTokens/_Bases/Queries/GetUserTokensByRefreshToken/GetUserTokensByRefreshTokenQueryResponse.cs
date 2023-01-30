using DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Dtos;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Queries.GetUserTokensByRefreshToken;

public class GetUserTokensByRefreshTokenQueryResponse
{
    public GetUserTokensByRefreshTokenQueryResponse()
    {
    }

    public GetUserTokensByRefreshTokenQueryResponse(UserTokenDto accessToken, UserTokenDto refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public UserTokenDto AccessToken { get; set; }
    public UserTokenDto RefreshToken { get; set; }
}