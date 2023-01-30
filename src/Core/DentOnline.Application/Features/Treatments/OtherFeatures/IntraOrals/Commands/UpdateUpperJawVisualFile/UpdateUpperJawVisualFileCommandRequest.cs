using Microsoft.AspNetCore.Http;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateUpperJawVisualFile;

public class UpdateUpperJawVisualFileCommandRequest : IRequest<IResponse>
{
    public string TreatmentId { get; set; }
    public IFormFile File { get; set; }
}