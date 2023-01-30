using System.Text.Json.Serialization;
using DentOnline.Application.Abstracts;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Dtos;
using DentOnline.Application.Features.Users._Bases.Dtos;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Dtos;

public class CommentDto : DtoBase
{
    [JsonIgnore] public string UserId { get; set; }
    public UserDto User { get; set; }

    public string Content { get; set; }

    public ICollection<FileDto> Files { get; set; }
}