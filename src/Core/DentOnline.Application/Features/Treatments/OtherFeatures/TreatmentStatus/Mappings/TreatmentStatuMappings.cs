using DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.CreateTreatmentStatu;
using DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.CreateTreatmentStatuToTreatment;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Mappings;

public class TreatmentStatuMappings : Profile
{
    public TreatmentStatuMappings()
    {
        CreateMap<CreateTreatmentStatuCommandRequest, TreatmentStatu>()
            .ForMember(x => x.CreatedDate, mopt => mopt.MapFrom(v => DateTime.Now))
            .ForMember(x => x.TreatmentStatuTypes, mopt => mopt.MapFrom(v => v.StatuType));

        CreateMap<CreateTreatmentStatuToTreatmentCommandRequest, CreateTreatmentStatuCommandRequest>();
    }
}