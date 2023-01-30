using DentOnline.Application.Features.Treatments._Bases.Commands.CreateTreatment;
using DentOnline.Application.Features.Treatments._Bases.Dtos;
using DentOnline.Domain.Concrete.Treatments._Bases;

namespace DentOnline.Application.Features.Treatments._Bases.Mappings;

public class TreatmentMappings : Profile
{
    public TreatmentMappings()
    {
        CreateMap<CreateTreatmentCommandRequest, Treatment>()
            .ForMember(x => x.CreatedDate, mopt => mopt.MapFrom(v => DateTime.Now));

        CreateMap<Treatment, TreatmentDto>()
            .ForMember(x => x.Comments, mopt => mopt.Ignore());
    }
}