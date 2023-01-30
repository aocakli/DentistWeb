using DentOnline.Domain.Abstractions;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations.Abstracts;
using MongoDB.Bson.Serialization.Attributes;
using File = DentOnline.Domain.Concrete.Treatments.OtherDomains.Files.File;

namespace DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations;

public class SickPeopleInformation : EmbeddedDocumentBase, ISickPeopleInformationBase
{
    [BsonElement("review-file")] public File? VisualFile { get; set; }
    [BsonElement("age")] public byte Age { get; set; }

    [BsonElement("gender")] public char Gender { get; set; }
}