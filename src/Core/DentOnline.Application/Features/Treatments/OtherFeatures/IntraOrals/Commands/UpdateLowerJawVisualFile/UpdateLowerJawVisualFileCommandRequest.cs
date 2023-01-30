using Microsoft.AspNetCore.Http;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateLowerJawVisualFile;

public class UpdateLowerJawVisualFileCommandRequest : IRequest<IResponse>
{
    public string TreatmentId { get; set; }
    public IFormFile File { get; set; }
}