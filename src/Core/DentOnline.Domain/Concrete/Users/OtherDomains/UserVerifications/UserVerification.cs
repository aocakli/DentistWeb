using DentOnline.Domain.Abstractions;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Abstracts;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications;

public class UserVerification : EmbeddedDocumentBase, IUserVerificationBase
{
    [BsonElement("is-used")] public bool IsUsed { get; set; }

    [BsonElement("verification-type")] public UserVerificationType VerificationType { get; set; }

    [BsonElement("code")] public string Code { get; set; }

    [BsonElement("expiry-date")] public DateTime ExpiryDate { get; set; }
}