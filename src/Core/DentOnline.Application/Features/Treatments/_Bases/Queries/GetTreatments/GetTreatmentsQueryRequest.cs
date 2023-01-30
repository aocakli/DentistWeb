using DentOnline.Application.Features.Treatments._Bases.Dtos;
using DentOnline.Application.Utilities.Paginations;

namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatments;

public class GetTreatmentsQueryRequest : IPaginationRequest, IRequest<IDataResponse<ICollection<TreatmentDto>>>
{
    public PaginationBase Pagination { get; set; }
}