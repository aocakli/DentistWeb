using DentOnline.Domain.Abstractions;
using DentOnline.Domain.Concrete.Users._Bases.Abstracts;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserTokens;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications;
using MongoDB.Bson.Serialization.Attributes;

namespace DentOnline.Domain.Concrete.Users._Bases;

public class User : DocumentBase, IUserBase
{
    [BsonElement("password")] public string Password { get; set; }

    [BsonElement("roles")] public HashSet<UserRoles> Roles { get; set; }

    [BsonElement("refresh-token")] public UserToken? RefreshToken { get; set; }

    [BsonElement("user-verifications")] public ICollection<UserVerification> UserVerifications { get; set; }
    [BsonElement("name")] public string Name { get; set; }

    [BsonElement("surname")] public string Surname { get; set; }

    [BsonElement("email")] public string Email { get; set; }
}