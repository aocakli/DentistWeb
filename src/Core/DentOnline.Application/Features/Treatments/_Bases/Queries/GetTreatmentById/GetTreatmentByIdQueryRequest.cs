using DentOnline.Application.Features.Treatments._Bases.Dtos;

namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentById;

public class GetTreatmentByIdQueryRequest : IRequest<IDataResponse<TreatmentDto>>
{
    public GetTreatmentByIdQueryRequest()
    {
    }

    public GetTreatmentByIdQueryRequest(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}