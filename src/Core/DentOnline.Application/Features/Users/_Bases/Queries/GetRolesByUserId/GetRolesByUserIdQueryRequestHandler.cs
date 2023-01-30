using DentOnline.Application.Constants;
using DentOnline.Application.Repositories.Users;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;

namespace DentOnline.Application.Features.Users._Bases.Queries.GetRolesByUserId;

public class
    GetRolesByUserIdQueryRequestHandler : IRequestHandler<GetRolesByUserIdQueryRequest,
        IDataResponse<HashSet<UserRoles>>>
{
    private readonly LanguageService _languageService;
    private readonly IUserReadRepository _readRepository;

    public GetRolesByUserIdQueryRequestHandler(IUserReadRepository readRepository, LanguageService languageService)
    {
        _readRepository = readRepository;
        _languageService = languageService;
    }

    public async Task<IDataResponse<HashSet<UserRoles>>> Handle(GetRolesByUserIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        var userRoles = await _readRepository.GetUserRolesByUserIdAsync(request.UserId);
        if (userRoles.Any() is false)
            return new ErrorDataResponse<HashSet<UserRoles>>(_languageService.Get(Messages.UserRolesAreNotFound));

        return new SuccessDataResponse<HashSet<UserRoles>>(_languageService.Get(Messages.UserRolesAreListed),
            userRoles);
    }
}