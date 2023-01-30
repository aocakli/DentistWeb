using DentOnline.Domain.Abstractions;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments.Abstracts;
using MongoDB.Bson.Serialization.Attributes;
using File = DentOnline.Domain.Concrete.Treatments.OtherDomains.Files.File;

namespace DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments;

public class Comment : EmbeddedDocumentBase, ICommentBase
{
    [BsonElement("files")] public ICollection<File> Files { get; set; }
    [BsonElement("user-id")] public string UserId { get; set; }

    [BsonElement("content")] public string Content { get; set; }
}