using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments.Abstracts;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Validators;

public class CommentBaseValidator<T> : AbstractValidator<T> where T : ICommentBase
{
    public CommentBaseValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotNull();
        RuleFor(x => x.Content).NotNull().NotNull().MinimumLength(5);
    }
}