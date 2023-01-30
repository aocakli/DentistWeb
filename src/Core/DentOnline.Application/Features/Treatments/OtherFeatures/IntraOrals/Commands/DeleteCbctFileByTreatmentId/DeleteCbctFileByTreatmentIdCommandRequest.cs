namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.DeleteCbctFileByTreatmentId;

public class DeleteCbctFileByTreatmentIdCommandRequest : IRequest<IResponse>
{
    public string TreatmentId { get; set; }
}