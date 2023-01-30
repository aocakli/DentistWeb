using DentOnline.Application.Constants;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    CreateSickPeopleInformation;

public class
    CreateSickPeopleInformationCommandRequestHandler : IRequestHandler<CreateSickPeopleInformationCommandRequest,
        IDataResponse<SickPeopleInformation>>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateSickPeopleInformationCommandRequestHandler(IMapper mapper, LanguageService languageService,
        IMediator mediator)
    {
        _mapper = mapper;
        _languageService = languageService;
        _mediator = mediator;
    }

    public async Task<IDataResponse<SickPeopleInformation>> Handle(CreateSickPeopleInformationCommandRequest request,
        CancellationToken cancellationToken)
    {
        var documentToAdd = _mapper.Map<CreateSickPeopleInformationCommandRequest, SickPeopleInformation>(request);

        if (request.CreateVisualFileCommand is not null)
        {
            var createdReviewFileResult = await _mediator.Send(request.CreateVisualFileCommand);
            if (createdReviewFileResult.IsSuccess)
                documentToAdd.VisualFile = createdReviewFileResult.Data.FirstOrDefault();
        }

        return new SuccessDataResponse<SickPeopleInformation>(
            _languageService.Get(Messages.SickPeopleInformationIsCreated), documentToAdd);
    }
}