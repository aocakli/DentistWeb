using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Queries.
    GetAvailableTreatmentStatusByStatu;

public class GetAvailableTreatmentStatusByStatuQueryRequest : IRequest<IDataResponse<HashSet<TreatmentStatuTypes>>>
{
    public GetAvailableTreatmentStatusByStatuQueryRequest()
    {
    }

    public GetAvailableTreatmentStatusByStatuQueryRequest(TreatmentStatuTypes treatmentStatuType)
    {
        TreatmentStatuType = treatmentStatuType;
    }

    public TreatmentStatuTypes TreatmentStatuType { get; set; }
}