using Microsoft.AspNetCore.Http;
using File = DentOnline.Domain.Concrete.Treatments.OtherDomains.Files.File;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.CreateFiles;

public class CreateFilesCommandRequest : IRequest<IDataResponse<ICollection<File>>>
{
    public CreateFilesCommandRequest()
    {
    }

    public CreateFilesCommandRequest(ICollection<IFormFile> files)
    {
        Files = files;
    }

    public CreateFilesCommandRequest(IFormFile file) : this(new List<IFormFile> { file })
    {
    }

    public ICollection<IFormFile> Files { get; set; }
}