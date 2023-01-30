using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;

namespace DentOnline.Application.Constants;

public class AuthRoles
{
    public const string Unknown = nameof(UserRoles.Unknown);
    public const string Doctor = nameof(UserRoles.Doctor);
    public const string Admin = nameof(UserRoles.Admin);
    public const string DoctorOrAdmin = $"{Doctor}, {Admin}";
}