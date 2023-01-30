using DentOnline.Application.Features.ClinicInformations.Commands.CreateClinicInformation;
using DentOnline.Domain.Concrete.ClinicInformations;

namespace DentOnline.Application.Features.ClinicInformations.Mappings;

public class ClinicInformationMappings : Profile
{
    public ClinicInformationMappings()
    {
        CreateMap<CreateClinicInformationCommandRequest, ClinicInformation>()
            .ForMember(x => x.CreatedDate, mopt => mopt.MapFrom(v => DateTime.UtcNow));
    }
}