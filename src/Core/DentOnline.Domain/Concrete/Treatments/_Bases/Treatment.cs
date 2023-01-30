using DentOnline.Domain.Abstractions;
using DentOnline.Domain.Concrete.Treatments._Bases.Abstracts;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.IntreOrals;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;
using MongoDB.Bson.Serialization.Attributes;

namespace DentOnline.Domain.Concrete.Treatments._Bases;

public class Treatment : DocumentBase, ITreatmentBase
{
    [BsonElement("intra-oral")] public IntraOral IntraOral { get; set; }
    [BsonElement("user-id-of-doctor")] public string UserIdOfDoctor { get; set; }

    [BsonElement("sick-people-information")]
    public SickPeopleInformation SickPeopleInformation { get; set; }

    [BsonElement("treatment-status")] public ICollection<TreatmentStatu> TreatmentStatuTimeLines { get; set; }

    [BsonElement("comments")] public ICollection<Comment> Comments { get; set; }

    [BsonElement("is-physical-measurement-sended")]
    public bool IsPhysicalMeasurementSended { get; set; }
}