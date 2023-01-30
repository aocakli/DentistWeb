using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Dtos;
using Microsoft.AspNetCore.Hosting;
using File = DentOnline.Domain.Concrete.Treatments.OtherDomains.Files.File;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.GenerateFileAtFile;

public class
    GenerateFileAtFileCommandRequestHandler : IRequestHandler<GenerateFileAtFileCommandRequest, IDataResponse<FileDto>>
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;

    public GenerateFileAtFileCommandRequestHandler(IWebHostEnvironment hostEnvironment, IMapper mapper,
        LanguageService languageService)
    {
        _hostEnvironment = hostEnvironment;
        _mapper = mapper;
        _languageService = languageService;
    }

    public async Task<IDataResponse<FileDto>> Handle(GenerateFileAtFileCommandRequest request,
        CancellationToken cancellationToken)
    {
        var file = request.File;

        var folderName = FolderConsts.FolderName;
        var saveLocation = $"{_hostEnvironment.WebRootPath}/{folderName}/";

        if (Directory.Exists(saveLocation) is false)
            Directory.CreateDirectory(saveLocation);

        var fileUrl = $@"{saveLocation}{file.RepresentativeName}";

        var fileDto = _mapper.Map<File, FileDto>(file);

        fileDto.Path = $"{folderName}/{file.RepresentativeName}";

        if (System.IO.File.Exists(fileUrl) is false)
            await System.IO.File.WriteAllBytesAsync(fileUrl, file?.FileAsByte);

        return new SuccessDataResponse<FileDto>(_languageService.Get(Messages.FilesAreGenerated), fileDto);
    }
}