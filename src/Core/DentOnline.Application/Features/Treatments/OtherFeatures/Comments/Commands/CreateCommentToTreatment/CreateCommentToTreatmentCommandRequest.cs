using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateCommentToTreatment;

public class CreateCommentToTreatmentCommandRequest : CreateCommentCommandRequest, IRequest<IResponse>
{
    public string TreatmentId { get; set; }
}