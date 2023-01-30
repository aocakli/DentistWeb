namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.GenerateIntraOral;

public class GenerateIntraOralCommandRequestValidator : AbstractValidator<GenerateIntraOralCommandRequest>
{
    public GenerateIntraOralCommandRequestValidator()
    {
        RuleFor(x => x.CreateUpperJawVisualFileCommand)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.CreateLowerJawVisualFileCommand)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.CreateClosingScanVisualFileCommand);

        RuleFor(x => x.CreateCephalometryVisualFileCommand)
            .NotNull()
            .When(x => x.CreateCbctVisualFileCommand is null);

        RuleFor(x => x.CreateCbctVisualFileCommand)
            .NotNull()
            .When(x => x.CreateCephalometryVisualFileCommand is null);
    }
}