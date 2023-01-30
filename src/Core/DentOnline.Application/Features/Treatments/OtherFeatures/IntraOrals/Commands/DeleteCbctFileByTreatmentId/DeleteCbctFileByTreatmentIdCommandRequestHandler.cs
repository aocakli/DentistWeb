using DentOnline.Application.Constants;
using DentOnline.Application.Repositories.Treatments._Bases;
using DentOnline.Application.Utilities.Exceptions;
using Microsoft.AspNetCore.Hosting;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.DeleteCbctFileByTreatmentId;

public class
    DeleteCbctFileByTreatmentIdCommandRequestHandler : IRequestHandler<DeleteCbctFileByTreatmentIdCommandRequest,
        IResponse>
{
    private readonly IWebHostEnvironment _environment;
    private readonly LanguageService _languageService;
    private readonly ITreatmentReadRepository _treatmentReadRepository;
    private readonly ITreatmentWriteRepository _treatmentWriteRepository;

    public DeleteCbctFileByTreatmentIdCommandRequestHandler(ITreatmentReadRepository treatmentReadRepository,
        LanguageService languageService, IWebHostEnvironment environment,
        ITreatmentWriteRepository treatmentWriteRepository)
    {
        _treatmentReadRepository = treatmentReadRepository;
        _languageService = languageService;
        _environment = environment;
        _treatmentWriteRepository = treatmentWriteRepository;
    }

    public async Task<IResponse> Handle(DeleteCbctFileByTreatmentIdCommandRequest request,
        CancellationToken cancellationToken)
    {
        var treatment =
            await _treatmentReadRepository.GetAsync(_treatment => request.TreatmentId.Equals(_treatment.Id));
        if (treatment is null)
            return new ErrorResponse(_languageService.Get(Messages.TreatmentIsNotFound));

        if (treatment.IntraOral.CbctVisualFileName is null)
            throw new BusinessException(_languageService.Get(Messages.TheTreatmentIsDontHaveCbctVisualFile));

        var cbctVisualFileName = treatment.IntraOral.CbctVisualFileName;

        var folderName = FolderConsts.FolderName;
        var saveLocation = $"{_environment.WebRootPath}/{folderName}";

        var filePath = string.Join("/", saveLocation, cbctVisualFileName);

        File.Delete(filePath);

        treatment.IntraOral.CbctVisualFileName = null;

        await _treatmentWriteRepository.UpdateAsync(request.TreatmentId, treatment);

        await _treatmentWriteRepository.SaveChangesAsync();

        return new SuccessResponse(_languageService.Get(Messages.CbctVisualFileIsDeleted));
    }
}