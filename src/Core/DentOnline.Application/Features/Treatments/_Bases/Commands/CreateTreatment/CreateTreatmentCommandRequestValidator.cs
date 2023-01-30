using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.GenerateIntraOral;
using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    CreateSickPeopleInformation;

namespace DentOnline.Application.Features.Treatments._Bases.Commands.CreateTreatment;

public class CreateTreatmentCommandRequestValidator : AbstractValidator<CreateTreatmentCommandRequest>
{
    public CreateTreatmentCommandRequestValidator(LanguageService languageService)
    {
        RuleFor(x => x.UserIdOfDoctor).NotNull().NotEmpty();

        RuleFor(x => x.CreateSickPeopleInformationCommand)
            .NotNull()
            .SetValidator(new CreateSickPeopleInformationCommandRequestValidator(languageService));

        RuleFor(x => x.CreateIntraOralCommand)
            .NotNull()
            .SetValidator(new GenerateIntraOralCommandRequestValidator());

        RuleFor(x => x.CreateCommentCommand)
            .NotNull()
            .NotEmpty()
            .SetValidator(new CreateCommentCommandRequestValidator());

        RuleFor(x => x.IsPhysicalMeasurementSended).NotNull();
    }
}