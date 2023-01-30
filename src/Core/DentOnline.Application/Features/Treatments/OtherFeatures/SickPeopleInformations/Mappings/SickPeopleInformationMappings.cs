using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    CreateSickPeopleInformation;
using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Dtos;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Mappings;

public class SickPeopleInformationMappings : Profile
{
    public SickPeopleInformationMappings()
    {
        CreateMap<CreateSickPeopleInformationCommandRequest, SickPeopleInformation>()
            .ForMember(x => x.CreatedDate, mopt => mopt.MapFrom(v => DateTime.Now));

        CreateMap<SickPeopleInformation, SickPeopleInformationDto>();
    }
}