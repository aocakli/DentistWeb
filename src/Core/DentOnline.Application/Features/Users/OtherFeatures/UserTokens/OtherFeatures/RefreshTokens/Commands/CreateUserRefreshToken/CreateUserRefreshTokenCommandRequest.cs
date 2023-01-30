using DentOnline.Domain.Concrete.Users._Bases;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.RefreshTokens.Commands.
    CreateUserRefreshToken;

public class CreateUserRefreshTokenCommandRequest : IRequest<IDataResponse<CreateUserRefreshTokenCommandResponse>>
{
    public CreateUserRefreshTokenCommandRequest()
    {
    }

    public CreateUserRefreshTokenCommandRequest(User user)
    {
        User = user;
    }

    public CreateUserRefreshTokenCommandRequest(string userId)
    {
        UserId = userId;
    }

    public User? User { get; set; }
    public string? UserId { get; set; }
}