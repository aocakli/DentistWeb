using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.CreateCbctVisualFile;
using DentOnline.Application.Repositories.Treatments._Bases;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateCbctVisualFile;

public class UpdateCbctVisualFileCommandRequestHandler : IRequestHandler<UpdateCbctVisualFileCommandRequest, IResponse>
{
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;
    private readonly ITreatmentReadRepository _treatmentRepository;
    private readonly ITreatmentWriteRepository _treatmentWriteRepository;

    public UpdateCbctVisualFileCommandRequestHandler(ITreatmentReadRepository treatmentRepository,
        LanguageService languageService, IMediator mediator, ITreatmentWriteRepository treatmentWriteRepository)
    {
        _treatmentRepository = treatmentRepository;
        _languageService = languageService;
        _mediator = mediator;
        _treatmentWriteRepository = treatmentWriteRepository;
    }

    public async Task<IResponse> Handle(UpdateCbctVisualFileCommandRequest request,
        CancellationToken cancellationToken)
    {
        var treatment = await _treatmentRepository.GetAsync(_treatment => _treatment.Id.Equals(request.TreatmentId));
        if (treatment is null)
            return new ErrorResponse(_languageService.Get(Messages.TreatmentIsNotFound));

        var createdCbctVisualFileResult = await _mediator.Send(new CreateCbctVisualFileCommandRequest(request.File));
        if (createdCbctVisualFileResult.IsSuccess is false)
            return new ErrorResponse(createdCbctVisualFileResult.Message);

        treatment.IntraOral.CbctVisualFileName = createdCbctVisualFileResult.Data;

        await _treatmentWriteRepository.UpdateAsync(request.TreatmentId, treatment);

        await _treatmentWriteRepository.SaveChangesAsync();

        return new SuccessResponse(_languageService.Get(Messages.CbctVisualFileIsUpdated));
    }
}