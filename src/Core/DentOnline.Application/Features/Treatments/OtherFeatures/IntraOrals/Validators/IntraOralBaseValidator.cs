using DentOnline.Domain.Concrete.Treatments.OtherDomains.IntreOrals.Abstracts;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Validators;

public class IntraOralBaseValidator<T> : AbstractValidator<T> where T : IIntraOralBase
{
    public IntraOralBaseValidator()
    {
        RuleFor(x => x.UpperJawVisualFile).NotNull().NotEmpty();

        RuleFor(x => x.LowerJawVisualFile).NotNull().NotEmpty();

        RuleFor(x => x.ClosingScanVisualFile);

        RuleFor(x => x.CephalometryVisualFile)
            .Must(x => x is not null)
            .When(x => string.IsNullOrEmpty(x.CbctVisualFileName));

        RuleFor(x => x.CbctVisualFileName)
            .Must(x => string.IsNullOrEmpty(x) is false)
            .When(x => x.CephalometryVisualFile is null);
    }
}