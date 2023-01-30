using DentOnline.Application.Constants;
using Microsoft.AspNetCore.Hosting;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.CreateCbctVisualFile;

public class
    CreateCbctVisualFileCommandRequestHandler : IRequestHandler<CreateCbctVisualFileCommandRequest,
        IDataResponse<string>>
{
    private readonly LanguageService _languageService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateCbctVisualFileCommandRequestHandler(IWebHostEnvironment webHostEnvironment,
        LanguageService languageService)
    {
        _webHostEnvironment = webHostEnvironment;
        _languageService = languageService;
    }

    public async Task<IDataResponse<string>> Handle(CreateCbctVisualFileCommandRequest request,
        CancellationToken cancellationToken)
    {
        var folderName = FolderConsts.FolderName;
        var saveLocation = $"{_webHostEnvironment.WebRootPath}/{folderName}";

        if (Directory.Exists(saveLocation) is false)
            Directory.CreateDirectory(saveLocation);

        var file = request.File;

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath = string.Join("/", saveLocation, fileName);

        using (var fs = new FileStream(filePath, FileMode.CreateNew))
        {
            await file.CopyToAsync(fs);
        }

        return new SuccessDataResponse<string>(_languageService.Get(Messages.CbctVisualFileIsCreated), fileName);
    }
}