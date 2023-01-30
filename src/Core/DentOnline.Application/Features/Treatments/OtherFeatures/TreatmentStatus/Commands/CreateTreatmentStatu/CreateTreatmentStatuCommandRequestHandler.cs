using DentOnline.Application.Constants;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.CreateTreatmentStatu;

public class
    CreateTreatmentStatuCommandRequestHandler : IRequestHandler<CreateTreatmentStatuCommandRequest,
        IDataResponse<TreatmentStatu>>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;

    public CreateTreatmentStatuCommandRequestHandler(IMapper mapper, LanguageService languageService)
    {
        _mapper = mapper;
        _languageService = languageService;
    }

    public async Task<IDataResponse<TreatmentStatu>> Handle(CreateTreatmentStatuCommandRequest request,
        CancellationToken cancellationToken)
    {
        var documentToAdd = _mapper.Map<CreateTreatmentStatuCommandRequest, TreatmentStatu>(request);

        return new SuccessDataResponse<TreatmentStatu>(_languageService.Get(Messages.TreatmentStatuIsCreated),
            documentToAdd);
    }
}