using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.CreateFiles;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations.Abstracts;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    CreateSickPeopleInformation;

public class CreateSickPeopleInformationCommandRequest : ISickPeopleInformationBase,
    IRequest<IDataResponse<SickPeopleInformation>>
{
    public CreateFilesCommandRequest? CreateVisualFileCommand { get; set; }
    public byte Age { get; set; }

    public char Gender { get; set; }
}