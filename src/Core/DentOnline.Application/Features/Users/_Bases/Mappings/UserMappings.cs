using DentOnline.Application.Features.Users._Bases.Commands.CreateUser;
using DentOnline.Application.Features.Users._Bases.Dtos;
using DentOnline.Application.Features.Users._Bases.Queries.LoginUser;
using DentOnline.Application.Features.Users.OtherFeatures.Admins.Commands.CreateAdmin;
using DentOnline.Application.Features.Users.OtherFeatures.Doctors.Commands.CreateDoctor;
using DentOnline.Domain.Concrete.Users._Bases;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;

namespace DentOnline.Application.Features.Users._Bases.Mappings;

public class UserMappings : Profile
{
    public UserMappings()
    {
        CreateMap<CreateUserCommandRequest, User>()
            .ForMember(x => x.CreatedDate, mopt => mopt.MapFrom(v => DateTime.Now));

        CreateMap<User, UserDto>()
            .ForMember(x => x.Role, mopt => mopt.MapFrom(v => new UserRoleDto(v.Roles)));

        CreateMap<UserDto, LoginUserQueryResponse>();

        CreateMap<CreateDoctorCommandRequest, CreateUserCommandRequest>()
            .ForMember(x => x.Roles, mopt => mopt.MapFrom(v => new List<UserRoles> { UserRoles.Doctor }));

        CreateMap<CreateAdminCommandRequest, CreateUserCommandRequest>()
            .ForMember(x => x.Roles, mopt => mopt.MapFrom(v => new List<UserRoles> { UserRoles.Admin }));
    }
}