using DentOnline.Application.Features.Users._Bases.Dtos;

namespace DentOnline.Application.Features.Users._Bases.Queries.GetUsers;

public class GetUsersQueryRequest : IRequest<IDataResponse<ICollection<UserDto>>>
{
}