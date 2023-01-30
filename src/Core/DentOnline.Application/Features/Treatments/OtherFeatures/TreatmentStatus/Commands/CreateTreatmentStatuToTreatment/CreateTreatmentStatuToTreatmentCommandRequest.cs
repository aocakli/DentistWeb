using DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.CreateTreatmentStatu;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.
    CreateTreatmentStatuToTreatment;

public class CreateTreatmentStatuToTreatmentCommandRequest : CreateTreatmentStatuCommandRequest, IRequest<IResponse>
{
    public CreateTreatmentStatuToTreatmentCommandRequest()
    {
    }

    public CreateTreatmentStatuToTreatmentCommandRequest(string treatmentId, TreatmentStatuTypes statuType) :
        base(statuType)
    {
        TreatmentId = treatmentId;
    }

    public string TreatmentId { get; set; }
}