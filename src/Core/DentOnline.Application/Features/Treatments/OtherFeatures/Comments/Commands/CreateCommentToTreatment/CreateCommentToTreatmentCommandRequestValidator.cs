using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateCommentToTreatment;

public class CreateCommentToTreatmentCommandRequestValidator : AbstractValidator<CreateCommentToTreatmentCommandRequest>
{
    public CreateCommentToTreatmentCommandRequestValidator()
    {
        RuleFor(x => x.TreatmentId).NotNull().NotEmpty();

        Include(new CreateCommentCommandRequestValidator());
    }
}