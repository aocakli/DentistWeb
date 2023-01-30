using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.CreateTreatmentStatu;

public class CreateTreatmentStatuCommandRequest : IRequest<IDataResponse<TreatmentStatu>>
{
    public CreateTreatmentStatuCommandRequest()
    {
    }

    public CreateTreatmentStatuCommandRequest(TreatmentStatuTypes statuType)
    {
        StatuType = statuType;
    }

    public TreatmentStatuTypes StatuType { get; set; }
}