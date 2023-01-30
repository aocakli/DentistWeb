using MongoDB.Bson.Serialization.Attributes;
using File = DentOnline.Domain.Concrete.Treatments.OtherDomains.Files.File;

namespace DentOnline.Domain.Concrete.Treatments.OtherDomains.IntreOrals.Abstracts;

public interface IIntraOralBase
{
    [BsonElement("upper-jaw-visual-file")] public File UpperJawVisualFile { get; set; }

    [BsonElement("lower-jaw-visual-file")] public File LowerJawVisualFile { get; set; }

    [BsonElement("cephalometry-visual-file")]
    public File? ClosingScanVisualFile { get; set; }

    [BsonElement("cephalometry-visual-file")]
    public File? CephalometryVisualFile { get; set; }

    [BsonElement("cbct-visual-file-name")] public string? CbctVisualFileName { get; set; }
}