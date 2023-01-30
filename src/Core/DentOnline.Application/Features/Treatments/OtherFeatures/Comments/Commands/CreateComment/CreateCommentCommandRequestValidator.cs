using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Validators;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;

public class CreateCommentCommandRequestValidator : AbstractValidator<CreateCommentCommandRequest>
{
    public CreateCommentCommandRequestValidator()
    {
        Include(new CommentBaseValidator<CreateCommentCommandRequest>());
    }
}