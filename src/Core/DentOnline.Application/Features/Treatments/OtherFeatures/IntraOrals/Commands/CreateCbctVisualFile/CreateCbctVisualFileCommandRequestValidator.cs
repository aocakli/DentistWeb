namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.CreateCbctVisualFile;

public class CreateCbctVisualFileCommandRequestValidator : AbstractValidator<CreateCbctVisualFileCommandRequest>
{
    public CreateCbctVisualFileCommandRequestValidator()
    {
        RuleFor(x => x.File).NotNull().NotEmpty();
    }
}