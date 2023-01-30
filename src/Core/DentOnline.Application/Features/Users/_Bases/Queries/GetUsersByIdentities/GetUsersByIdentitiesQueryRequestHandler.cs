using DentOnline.Application.Constants;
using DentOnline.Application.Features.Users._Bases.Dtos;
using DentOnline.Application.Repositories.Users;
using DentOnline.Domain.Concrete.Users._Bases;

namespace DentOnline.Application.Features.Users._Bases.Queries.GetUsersByIdentities;

public class
    GetUsersByIdentitiesQueryRequestHandler : IRequestHandler<GetUsersByIdentitiesQueryRequest,
        IDataResponse<ICollection<UserDto>>>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IUserReadRepository _readRepository;

    public GetUsersByIdentitiesQueryRequestHandler(IUserReadRepository readRepository, IMapper mapper,
        LanguageService languageService)
    {
        _readRepository = readRepository;
        _mapper = mapper;
        _languageService = languageService;
    }

    public async Task<IDataResponse<ICollection<UserDto>>> Handle(GetUsersByIdentitiesQueryRequest request,
        CancellationToken cancellationToken)
    {
        var users = await _readRepository.GetAllAsync(_user => request.Identities.Contains(_user.Id));
        if (users.Any() is false)
            return new ErrorDataResponse<ICollection<UserDto>>(_languageService.Get(Messages.UsersAreNotFound));

        var userDtos = _mapper.Map<ICollection<User>, ICollection<UserDto>>(users);

        return new SuccessDataResponse<ICollection<UserDto>>(_languageService.Get(Messages.UsersAreListed), userDtos);
    }
}