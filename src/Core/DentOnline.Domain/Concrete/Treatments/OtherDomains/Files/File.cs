using DentOnline.Domain.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace DentOnline.Domain.Concrete.Treatments.OtherDomains.Files;

public class File : EmbeddedDocumentBase
{
    [BsonElement("file-as-byte")] public byte[] FileAsByte { get; set; }

    /// Byte'dan klasöre kayıt edilirken bu isim kullanılır.
    public string RepresentativeName { get; set; }
}