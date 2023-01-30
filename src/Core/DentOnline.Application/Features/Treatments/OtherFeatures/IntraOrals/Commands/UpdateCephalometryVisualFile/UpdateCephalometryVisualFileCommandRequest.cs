using Microsoft.AspNetCore.Http;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateCephalometryVisualFile;

public class UpdateCephalometryVisualFileCommandRequest : IRequest<IResponse>
{
    public string TreatmentId { get; set; }
    public IFormFile File { get; set; }
}