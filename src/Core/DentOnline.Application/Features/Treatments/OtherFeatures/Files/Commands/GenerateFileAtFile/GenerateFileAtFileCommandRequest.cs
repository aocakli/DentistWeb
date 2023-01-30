using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Dtos;
using File = DentOnline.Domain.Concrete.Treatments.OtherDomains.Files.File;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.GenerateFileAtFile;

public class GenerateFileAtFileCommandRequest : IRequest<IDataResponse<FileDto>>
{
    public GenerateFileAtFileCommandRequest()
    {
    }

    public GenerateFileAtFileCommandRequest(File file)
    {
        File = file;
    }

    public File File { get; set; }
}