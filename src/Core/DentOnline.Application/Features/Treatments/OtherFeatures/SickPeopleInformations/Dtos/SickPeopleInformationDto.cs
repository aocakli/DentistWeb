using DentOnline.Application.Abstracts;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Dtos;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations.Abstracts;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Dtos;

public class SickPeopleInformationDto : DtoBase, ISickPeopleInformationBase
{
    public FileDto? ReviewFile { get; set; }
    public byte Age { get; set; }

    public char Gender { get; set; }
}