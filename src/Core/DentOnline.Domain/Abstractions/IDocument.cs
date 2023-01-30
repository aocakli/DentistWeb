namespace DentOnline.Domain.Abstractions;

public interface IDocument : IDocumentBase
{
    public string Id { get; set; }
}