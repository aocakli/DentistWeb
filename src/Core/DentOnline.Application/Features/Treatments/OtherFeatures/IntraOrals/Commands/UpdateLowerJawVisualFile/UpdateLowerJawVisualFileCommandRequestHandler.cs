using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.CreateFiles;
using DentOnline.Application.Repositories.Treatments._Bases;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateLowerJawVisualFile;

public class
    UpdateLowerJawVisualFileCommandRequestHandler : IRequestHandler<UpdateLowerJawVisualFileCommandRequest, IResponse>
{
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;
    private readonly ITreatmentReadRepository _treatmentRepository;
    private readonly ITreatmentWriteRepository _treatmentWriteRepository;

    public UpdateLowerJawVisualFileCommandRequestHandler(ITreatmentReadRepository treatmentRepository,
        LanguageService languageService, IMediator mediator, ITreatmentWriteRepository treatmentWriteRepository)
    {
        _treatmentRepository = treatmentRepository;
        _languageService = languageService;
        _mediator = mediator;
        _treatmentWriteRepository = treatmentWriteRepository;
    }

    public async Task<IResponse> Handle(UpdateLowerJawVisualFileCommandRequest request,
        CancellationToken cancellationToken)
    {
        var treatment = await _treatmentRepository.GetAsync(_treatment => _treatment.Id.Equals(request.TreatmentId));
        if (treatment is null)
            return new ErrorResponse(_languageService.Get(Messages.TreatmentIsNotFound));

        var createdFileResult = await _mediator.Send(new CreateFilesCommandRequest(request.File));
        if (createdFileResult.IsSuccess is false)
            return new ErrorResponse(createdFileResult.Message);

        treatment.IntraOral.LowerJawVisualFile = createdFileResult.Data.First();

        await _treatmentWriteRepository.UpdateAsync(request.TreatmentId, treatment);

        await _treatmentWriteRepository.SaveChangesAsync();

        return new SuccessResponse(_languageService.Get(Messages.LowerJawVisualFileIsUpdated));
    }
}