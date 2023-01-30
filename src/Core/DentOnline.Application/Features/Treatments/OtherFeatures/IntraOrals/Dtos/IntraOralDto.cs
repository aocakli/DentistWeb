using DentOnline.Application.Abstracts;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Dtos;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Dtos;

public class IntraOralDto : IDto
{
    public FileDto UpperJawVisualFile { get; set; }

    public FileDto LowerJawVisualFile { get; set; }

    public FileDto? ClosingScanVisualFile { get; set; }

    public FileDto? CephalometryVisualFile { get; set; }

    public string? CbctVisualFileUrl { get; set; }

    public DateTime CreatedDate { get; set; }
}