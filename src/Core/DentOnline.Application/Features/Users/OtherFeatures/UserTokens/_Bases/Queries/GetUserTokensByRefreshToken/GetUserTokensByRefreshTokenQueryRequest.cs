namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Queries.GetUserTokensByRefreshToken;

public class GetUserTokensByRefreshTokenQueryRequest : IRequest<IDataResponse<GetUserTokensByRefreshTokenQueryResponse>>
{
    public string UserId { get; set; }
    public string RefreshToken { get; set; }
}