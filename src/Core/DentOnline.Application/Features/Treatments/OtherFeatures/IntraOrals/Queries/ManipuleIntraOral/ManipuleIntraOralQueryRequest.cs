using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Dtos;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.IntreOrals;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Queries.ManipuleIntraOral;

public class ManipuleIntraOralQueryRequest : IRequest<IDataResponse<IntraOralDto>>
{
    public ManipuleIntraOralQueryRequest()
    {
    }

    public ManipuleIntraOralQueryRequest(IntraOral ıntraOral)
    {
        IntraOral = ıntraOral;
    }

    public IntraOral IntraOral { get; set; }
}