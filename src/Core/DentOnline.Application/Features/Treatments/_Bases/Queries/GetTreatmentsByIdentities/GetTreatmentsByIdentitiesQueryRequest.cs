using DentOnline.Application.Features.Treatments._Bases.Dtos;

namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByIdentities;

public class
    GetTreatmentsByIdentitiesQueryRequest : IRequest<IDataResponse<ICollection<TreatmentDto>>>
{
    public GetTreatmentsByIdentitiesQueryRequest()
    {
    }

    public GetTreatmentsByIdentitiesQueryRequest(HashSet<string> identities)
    {
        Identities = identities;
    }

    public GetTreatmentsByIdentitiesQueryRequest(string id) : this(new HashSet<string> { id })
    {
    }

    public HashSet<string> Identities { get; set; }
}