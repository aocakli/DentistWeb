using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Dtos;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Queries.
    ManipuleSickPeopleInformation;

public class ManipuleSickPeopleInformationQueryRequest : IRequest<IDataResponse<SickPeopleInformationDto>>
{
    public ManipuleSickPeopleInformationQueryRequest()
    {
    }

    public ManipuleSickPeopleInformationQueryRequest(SickPeopleInformation sickPeopleInformation)
    {
        SickPeopleInformation = sickPeopleInformation;
    }

    public SickPeopleInformation SickPeopleInformation { get; set; }
}