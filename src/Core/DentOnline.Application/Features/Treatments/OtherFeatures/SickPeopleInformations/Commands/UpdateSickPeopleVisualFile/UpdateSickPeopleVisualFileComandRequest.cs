using Microsoft.AspNetCore.Http;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    UpdateSickPeopleVisualFile;

public class UpdateSickPeopleVisualFileCommandRequest : IRequest<IResponse>
{
    public string TreatmentId { get; set; }
    public IFormFile File { get; set; }
}