using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Queries.
    GetAvailableTreatmentStatusByStatu;

public class
    GetAvailableTreatmentStatusByStatuQueryRequestHandler : IRequestHandler<
        GetAvailableTreatmentStatusByStatuQueryRequest, IDataResponse<HashSet<TreatmentStatuTypes>>>
{
    public async Task<IDataResponse<HashSet<TreatmentStatuTypes>>> Handle(
        GetAvailableTreatmentStatusByStatuQueryRequest request,
        CancellationToken cancellationToken)
    {
        List<TreatmentStatuTypes> availableTreatmentStatus = new();

        switch (request.TreatmentStatuType)
        {
            case TreatmentStatuTypes.ReviewPhase:
                availableTreatmentStatus.AddRange(new[]
                {
                    TreatmentStatuTypes.Cancelled,
                    TreatmentStatuTypes.PlanningPhase
                });
                break;
            case TreatmentStatuTypes.PlanningPhase:
                availableTreatmentStatus.AddRange(new[]
                {
                    TreatmentStatuTypes.Cancelled,
                    TreatmentStatuTypes.ReviewPhase,
                    TreatmentStatuTypes.ProductionPhase
                });
                break;
            case TreatmentStatuTypes.ProductionPhase:
                availableTreatmentStatus.AddRange(new[]
                {
                    TreatmentStatuTypes.Cancelled,
                    TreatmentStatuTypes.PlanningPhase,
                    TreatmentStatuTypes.TreatmentFinished
                });
                break;
        }

        return new SuccessDataResponse<HashSet<TreatmentStatuTypes>>(string.Empty,
            availableTreatmentStatus.ToHashSet());
    }
}