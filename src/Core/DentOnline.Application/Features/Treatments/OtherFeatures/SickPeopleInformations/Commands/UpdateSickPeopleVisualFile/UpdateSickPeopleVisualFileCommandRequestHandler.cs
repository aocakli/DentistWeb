using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.CreateFiles;
using DentOnline.Application.Repositories.Treatments._Bases;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    UpdateSickPeopleVisualFile;

public class
    UpdateSickPeopleVisualFileCommandRequestHandler : IRequestHandler<UpdateSickPeopleVisualFileCommandRequest,
        IResponse>
{
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;
    private readonly ITreatmentReadRepository _treatmentReadRepository;
    private readonly ITreatmentWriteRepository _treatmentWriteRepository;

    public UpdateSickPeopleVisualFileCommandRequestHandler(ITreatmentReadRepository treatmentReadRepository,
        ITreatmentWriteRepository treatmentWriteRepository, LanguageService languageService, IMediator mediator)
    {
        _treatmentReadRepository = treatmentReadRepository;
        _treatmentWriteRepository = treatmentWriteRepository;
        _languageService = languageService;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(UpdateSickPeopleVisualFileCommandRequest request,
        CancellationToken cancellationToken)
    {
        var treatment =
            await _treatmentReadRepository.GetAsync(_treatment => _treatment.Id.Equals(request.TreatmentId));
        if (treatment is null)
            return new ErrorResponse(_languageService.Get(Messages.TreatmentIsNotFound));

        var createdFileResult = await _mediator.Send(new CreateFilesCommandRequest(request.File));
        if (createdFileResult.IsSuccess is false)
            return new ErrorResponse(createdFileResult.Message);

        treatment.SickPeopleInformation.VisualFile = createdFileResult.Data.First();

        await _treatmentWriteRepository.UpdateAsync(request.TreatmentId, treatment);

        await _treatmentWriteRepository.SaveChangesAsync();

        return new SuccessResponse(_languageService.Get(Messages.SickPeopleVisualFileIsUpdated));
    }
}