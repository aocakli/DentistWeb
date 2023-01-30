namespace DentOnline.Application.Utilities.Paginations;

public interface IPaginationRequest
{
    public PaginationBase Pagination { get; set; }
}