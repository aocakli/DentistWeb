using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Dtos;
using File = DentOnline.Domain.Concrete.Treatments.OtherDomains.Files.File;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Files.Mappings;

public class FileMappings : Profile
{
    public FileMappings()
    {
        CreateMap<File, FileDto>();
    }
}