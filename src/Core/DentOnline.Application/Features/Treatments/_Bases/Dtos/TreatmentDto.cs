using DentOnline.Application.Abstracts;
using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Dtos;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Dtos;
using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Dtos;
using DentOnline.Application.Features.Users._Bases.Dtos;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Application.Features.Treatments._Bases.Dtos;

public class TreatmentDto : DtoBase
{
    public string Id { get; set; }

    // public string UserIdOfDoctor { get; set; }

    public UserDto? Doctor { get; set; }

    public SickPeopleInformationDto SickPeopleInformation { get; set; }
    public IntraOralDto IntraOral { get; set; }

    public ICollection<TreatmentStatu> TreatmentStatuTimeLines { get; set; }
    public ICollection<TreatmentStatuTypes> AvailableTreatmentStatus { get; set; }

    public ICollection<CommentDto> Comments { get; set; }
}