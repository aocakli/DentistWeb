using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.GenerateFileAtFile;
using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Dtos;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Queries.
    ManipuleSickPeopleInformation;

public class
    ManipuleSickPeopleInformationQueryRequestHandler : IRequestHandler<ManipuleSickPeopleInformationQueryRequest,
        IDataResponse<SickPeopleInformationDto>>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ManipuleSickPeopleInformationQueryRequestHandler(IMapper mapper, IMediator mediator,
        LanguageService languageService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _languageService = languageService;
    }

    public async Task<IDataResponse<SickPeopleInformationDto>> Handle(ManipuleSickPeopleInformationQueryRequest request,
        CancellationToken cancellationToken)
    {
        var sickPeopleInformationDto =
            _mapper.Map<SickPeopleInformation, SickPeopleInformationDto>(request.SickPeopleInformation);

        if (request.SickPeopleInformation.VisualFile is not null)
        {
            var generatedFileResult =
                await _mediator.Send(new GenerateFileAtFileCommandRequest(request.SickPeopleInformation.VisualFile));
            if (generatedFileResult.IsSuccess is false)
                return new ErrorDataResponse<SickPeopleInformationDto>(generatedFileResult.Message);

            sickPeopleInformationDto.ReviewFile = generatedFileResult.Data;
        }

        return new SuccessDataResponse<SickPeopleInformationDto>(
            _languageService.Get(Messages.SickPeopleInformationIsManipulated),
            sickPeopleInformationDto);
    }
}