using DentOnline.Domain.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

public class TreatmentStatu : EmbeddedDocumentBase
{
    [BsonElement("treatment-status")] public TreatmentStatuTypes TreatmentStatuTypes { get; set; }
}