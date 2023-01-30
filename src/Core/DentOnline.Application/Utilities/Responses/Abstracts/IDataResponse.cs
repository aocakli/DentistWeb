namespace DentOnline.Application.Utilities.Responses.Abstracts;

public interface IDataResponse<T> : IResponse
{
    public T Data { get; set; }
}