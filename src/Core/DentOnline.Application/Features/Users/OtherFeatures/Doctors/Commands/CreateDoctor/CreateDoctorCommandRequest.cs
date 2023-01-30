using DentOnline.Application.Features.ClinicInformations.Commands.CreateClinicInformation;
using DentOnline.Application.Features.Users._Bases.Commands.CreateUser;

namespace DentOnline.Application.Features.Users.OtherFeatures.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandRequest : CreateUserCommandRequest, IRequest<IResponse>
{
    public CreateClinicInformationCommandRequest CreateClinicInformationCommandRequest { get; set; }
}