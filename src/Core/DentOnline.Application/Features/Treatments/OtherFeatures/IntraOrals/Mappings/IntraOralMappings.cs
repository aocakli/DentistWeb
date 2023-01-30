using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Dtos;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.IntreOrals;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Mappings;

public class IntraOralMappings : Profile
{
    public IntraOralMappings()
    {
        CreateMap<IntraOral, IntraOralDto>();
    }
}