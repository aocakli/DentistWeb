using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.CreateFiles;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments.Abstracts;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;

public class CreateCommentCommandRequest : ICommentBase, IRequest<IDataResponse<Comment>>
{
    public CreateFilesCommandRequest? CreateFilesCommand { get; set; }
    public string UserId { get; set; }

    public string Content { get; set; }
}