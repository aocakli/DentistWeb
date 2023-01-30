using DentOnline.Application.Features.Treatments._Bases.Dtos;

namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByDoctorId;

public class GetTreatmentsByDoctorIdQueryRequest : IRequest<IDataResponse<ICollection<TreatmentDto>>>
{
    public string DoctorId { get; set; }
}