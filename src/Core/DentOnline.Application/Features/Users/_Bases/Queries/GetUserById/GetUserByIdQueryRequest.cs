using DentOnline.Application.Features.Users._Bases.Dtos;

namespace DentOnline.Application.Features.Users._Bases.Queries.GetUserById;

public class GetUserByIdQueryRequest : IRequest<IDataResponse<UserDto>>
{
    public GetUserByIdQueryRequest()
    {
    }

    public GetUserByIdQueryRequest(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}