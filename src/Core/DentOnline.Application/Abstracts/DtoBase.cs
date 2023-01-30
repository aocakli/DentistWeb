namespace DentOnline.Application.Abstracts;

public abstract class DtoBase : IDto
{
    public DateTime CreatedDate { get; set; }
}