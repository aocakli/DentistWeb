using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.GenerateIntraOral;
using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    CreateSickPeopleInformation;
using DentOnline.Domain.Concrete.Treatments._Bases;

namespace DentOnline.Application.Features.Treatments._Bases.Commands.CreateTreatment;

public class CreateTreatmentCommandRequest : IRequest<IDataResponse<Treatment>>
{
    public string UserIdOfDoctor { get; set; }
    public CreateCommentCommandRequest CreateCommentCommand { get; set; }
    public CreateSickPeopleInformationCommandRequest CreateSickPeopleInformationCommand { get; set; }

    public GenerateIntraOralCommandRequest CreateIntraOralCommand { get; set; }

    public bool IsPhysicalMeasurementSended { get; set; }
}