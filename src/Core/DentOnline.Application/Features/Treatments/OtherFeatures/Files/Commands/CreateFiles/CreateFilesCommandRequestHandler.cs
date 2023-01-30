using DentOnline.Application.Constants;
using File = DentOnline.Domain.Concrete.Treatments.OtherDomains.Files.File;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.CreateFiles;

public class
    CreateFilesCommandRequestHandler : IRequestHandler<CreateFilesCommandRequest,
        IDataResponse<ICollection<File>>>
{
    private readonly LanguageService _languageService;

    public CreateFilesCommandRequestHandler(LanguageService languageService)
    {
        _languageService = languageService;
    }

    public async Task<IDataResponse<ICollection<File>>> Handle(
        CreateFilesCommandRequest request,
        CancellationToken cancellationToken)
    {
        List<File> documents = new();

        foreach (var file in request.Files)
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);

                File document = new()
                {
                    FileAsByte = ms.ToArray(),
                    CreatedDate = DateTime.Now,
                    RepresentativeName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"
                };

                documents.Add(document);
            }

        return new SuccessDataResponse<ICollection<File>>(_languageService.Get(Messages.FilesAreConvertedToBinary),
            documents);
    }
}