using DentOnline.Application.Features.Users._Bases.Dtos;

namespace DentOnline.Application.Features.Users._Bases.Queries.GetUsersByIdentities;

public class GetUsersByIdentitiesQueryRequest : IRequest<IDataResponse<ICollection<UserDto>>>
{
    public GetUsersByIdentitiesQueryRequest()
    {
    }

    public GetUsersByIdentitiesQueryRequest(HashSet<string> ıdentities)
    {
        Identities = ıdentities;
    }

    public GetUsersByIdentitiesQueryRequest(string id) : this(new HashSet<string> { id })
    {
    }

    public HashSet<string> Identities { get; set; }
}