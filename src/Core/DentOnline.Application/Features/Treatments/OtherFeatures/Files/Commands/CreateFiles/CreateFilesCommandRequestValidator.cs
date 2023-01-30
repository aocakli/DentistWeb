namespace DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.CreateFiles;

public class CreateFilesCommandRequestValidator : AbstractValidator<CreateFilesCommandRequest>
{
    public CreateFilesCommandRequestValidator()
    {
        RuleFor(x => x.Files).NotNull().NotEmpty();
    }
}