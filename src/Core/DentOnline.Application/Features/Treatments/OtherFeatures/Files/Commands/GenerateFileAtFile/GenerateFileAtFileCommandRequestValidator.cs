namespace DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.GenerateFileAtFile;

public class GenerateFileAtFileCommandRequestValidator : AbstractValidator<GenerateFileAtFileCommandRequest>
{
    public GenerateFileAtFileCommandRequestValidator()
    {
        RuleFor(x => x.File).NotNull().NotEmpty();
    }
}