namespace DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments.Abstracts;

public interface ICommentBase
{
    public string UserId { get; set; }
    public string Content { get; set; }
}