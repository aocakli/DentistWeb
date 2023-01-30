using Microsoft.AspNetCore.Http;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateCbctVisualFile;

public class UpdateCbctVisualFileCommandRequest : IRequest<IResponse>
{
    public string TreatmentId { get; set; }
    public IFormFile File { get; set; }
}