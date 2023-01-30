using Microsoft.AspNetCore.Http;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.CreateCbctVisualFile;

public class CreateCbctVisualFileCommandRequest : IRequest<IDataResponse<string>>
{
    public CreateCbctVisualFileCommandRequest()
    {
    }

    public CreateCbctVisualFileCommandRequest(IFormFile file)
    {
        File = file;
    }

    public IFormFile File { get; set; }
}