using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;

namespace DentOnline.Application.Features.Users._Bases.Queries.GetRolesByUserId;

public class GetRolesByUserIdQueryRequest : IRequest<IDataResponse<HashSet<UserRoles>>>
{
    public GetRolesByUserIdQueryRequest()
    {
    }

    public GetRolesByUserIdQueryRequest(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}