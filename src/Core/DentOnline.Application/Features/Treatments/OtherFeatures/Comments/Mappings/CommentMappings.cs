using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;
using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateCommentToTreatment;
using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Dtos;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments;
using MongoDB.Bson;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Mappings;

public class CommentMappings : Profile
{
    public CommentMappings()
    {
        CreateMap<CreateCommentCommandRequest, Comment>()
            .ForMember(x => x.CreatedDate, mopt => mopt.MapFrom(v => DateTime.Now))
            .ForMember(x => x.UserId, mopt => mopt.MapFrom(v => ObjectId.Parse(v.UserId)));

        CreateMap<Comment, CommentDto>()
            .ForMember(x => x.Files, mopt => mopt.Ignore());

        CreateMap<CreateCommentToTreatmentCommandRequest, CreateCommentCommandRequest>();
    }
}