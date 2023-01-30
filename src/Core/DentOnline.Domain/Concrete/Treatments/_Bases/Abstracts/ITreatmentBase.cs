using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Domain.Concrete.Treatments._Bases.Abstracts;

public interface ITreatmentBase
{
    public string UserIdOfDoctor { get; set; }
    public SickPeopleInformation SickPeopleInformation { get; set; }
    public ICollection<TreatmentStatu> TreatmentStatuTimeLines { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public bool IsPhysicalMeasurementSended { get; set; }
}