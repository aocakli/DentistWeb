using DentOnline.Application.Features.Users._Bases.Commands.CreateUser;

namespace DentOnline.Application.Features.Users.OtherFeatures.Admins.Commands.CreateAdmin;

public class CreateAdminCommandRequest : CreateUserCommandRequest, IRequest<IResponse>
{
}