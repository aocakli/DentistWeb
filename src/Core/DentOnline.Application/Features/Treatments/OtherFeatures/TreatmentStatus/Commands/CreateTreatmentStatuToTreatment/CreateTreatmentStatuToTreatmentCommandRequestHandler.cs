using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.CreateTreatmentStatu;
using DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Queries.
    GetAvailableTreatmentStatusByStatu;
using DentOnline.Application.Repositories.Treatments._Bases;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.
    CreateTreatmentStatuToTreatment;

public class
    CreateTreatmentStatuToTreatmentCommandRequestHandler : IRequestHandler<CreateTreatmentStatuToTreatmentCommandRequest
        , IResponse>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITreatmentReadRepository _readRepository;
    private readonly ITreatmentWriteRepository _treatmentWriteRepository;

    public CreateTreatmentStatuToTreatmentCommandRequestHandler(ITreatmentReadRepository readRepository,
        IMediator mediator, ITreatmentWriteRepository treatmentWriteRepository, IMapper mapper,
        LanguageService languageService)
    {
        _readRepository = readRepository;
        _mediator = mediator;
        _treatmentWriteRepository = treatmentWriteRepository;
        _mapper = mapper;
        _languageService = languageService;
    }

    public async Task<IResponse> Handle(CreateTreatmentStatuToTreatmentCommandRequest request,
        CancellationToken cancellationToken)
    {
        var treatment = await _readRepository.GetAsync(_treatment => request.TreatmentId.Equals(_treatment.Id));
        if (treatment is null)
            return new ErrorResponse(_languageService.Get(Messages.TreatmentIsNotFound));

        var currentTreatmentStatu = treatment.TreatmentStatuTimeLines.Last().TreatmentStatuTypes;

        var availableTreatmentStatusResult = await _mediator.Send(
            new GetAvailableTreatmentStatusByStatuQueryRequest(currentTreatmentStatu));
        if (availableTreatmentStatusResult.IsSuccess is false)
            return new ErrorResponse(availableTreatmentStatusResult.Message);

        if (availableTreatmentStatusResult.Data.Contains(request.StatuType) is false)
        {
            var content =
                string.Format(_languageService.Get(Messages.TreatmentStatuCanNotBeAddedAfterTreatmentStatu),
                    request.StatuType.ToString(), currentTreatmentStatu);

            throw new BusinessException(content);
        }

        var createTreatmentStatuCommandRequest =
            _mapper.Map<CreateTreatmentStatuToTreatmentCommandRequest, CreateTreatmentStatuCommandRequest>(request);

        var createdTreatmentStatuResult = await _mediator.Send(createTreatmentStatuCommandRequest);
        if (createdTreatmentStatuResult.IsSuccess is false)
            return new ErrorResponse(createdTreatmentStatuResult.Message);

        if (treatment.TreatmentStatuTimeLines is null)
            treatment.TreatmentStatuTimeLines = new List<TreatmentStatu>();

        treatment.TreatmentStatuTimeLines.Add(createdTreatmentStatuResult.Data);

        await _treatmentWriteRepository.UpdateAsync(treatment.Id, treatment);

        if (await _treatmentWriteRepository.SaveChangesAsync() is false)
            throw new ErrorException(Messages.TreatmentStatuIsNotAddedToTreatment);

        return new SuccessResponse(_languageService.Get(Messages.TreatmentStatuIsCreated));
    }
}