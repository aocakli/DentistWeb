namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.AccessTokens.Queries.
    GenerateUserAccessToken;

public class GenerateUserAccessTokenQueryRequest : IRequest<IDataResponse<GenerateUserAccessTokenQueryResponse>>
{
    public GenerateUserAccessTokenQueryRequest()
    {
    }

    public GenerateUserAccessTokenQueryRequest(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}