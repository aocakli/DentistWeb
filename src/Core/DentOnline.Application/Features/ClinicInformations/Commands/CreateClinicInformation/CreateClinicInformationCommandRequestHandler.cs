using DentOnline.Application.Constants;
using DentOnline.Application.Repositories.ClinicInformations;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Domain.Concrete.ClinicInformations;

namespace DentOnline.Application.Features.ClinicInformations.Commands.CreateClinicInformation;

public class
    CreateClinicInformationCommandRequestHandler : IRequestHandler<CreateClinicInformationCommandRequest,
        IDataResponse<ClinicInformation>>
{
    private readonly IClinicInformationWriteRepository _clinicInformationWriteRepository;
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;

    public CreateClinicInformationCommandRequestHandler(
        IClinicInformationWriteRepository clinicInformationWriteRepository, IMapper mapper,
        LanguageService languageService)
    {
        _clinicInformationWriteRepository = clinicInformationWriteRepository;
        _mapper = mapper;
        _languageService = languageService;
    }

    public async Task<IDataResponse<ClinicInformation>> Handle(CreateClinicInformationCommandRequest request,
        CancellationToken cancellationToken)
    {
        var entityToAdd = _mapper.Map<CreateClinicInformationCommandRequest, ClinicInformation>(request);

        await _clinicInformationWriteRepository.CreateAsync(entityToAdd);

        if (request.IsSaveChanges && await _clinicInformationWriteRepository.SaveChangesAsync() is false)
            throw new ErrorException(_languageService.Get(Messages.ClinicInformationIsNotCreated));

        return new SuccessDataResponse<ClinicInformation>(_languageService.Get(Messages.ClinicInformationIsCreated),
            entityToAdd);
    }
}