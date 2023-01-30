using DentOnline.Application.Constants;
using DentOnline.Application.Features.Users._Bases.Dtos;
using DentOnline.Application.Features.Users._Bases.Queries.GetUsersByIdentities;
using DentOnline.Application.Repositories.Users;

namespace DentOnline.Application.Features.Users._Bases.Queries.GetUsers;

public class GetUsersQueryRequestHandler : IRequestHandler<GetUsersQueryRequest, IDataResponse<ICollection<UserDto>>>
{
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;
    private readonly IUserReadRepository _readRepository;

    public GetUsersQueryRequestHandler(IUserReadRepository readRepository, IMediator mediator,
        LanguageService languageService)
    {
        _readRepository = readRepository;
        _mediator = mediator;
        _languageService = languageService;
    }

    public async Task<IDataResponse<ICollection<UserDto>>> Handle(GetUsersQueryRequest request,
        CancellationToken cancellationToken)
    {
        var userIdentities = await _readRepository.GetIdentitiesAsync();
        if (userIdentities.Any() is false)
            return new ErrorDataResponse<ICollection<UserDto>>(_languageService.Get(Messages.UsersAreNotFound));

        var usersResult = await _mediator.Send(new GetUsersByIdentitiesQueryRequest(userIdentities));
        if (usersResult.IsSuccess is false)
            return new ErrorDataResponse<ICollection<UserDto>>(usersResult.Message);

        return new SuccessDataResponse<ICollection<UserDto>>(_languageService.Get(Messages.UsersAreListed),
            usersResult.Data);
    }
}