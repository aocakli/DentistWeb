namespace DentOnline.Domain.Concrete.ClinicInformations.Abstracts;

public interface IClinicInformationBase
{
    public string TelephoneNumber { get; set; }
    public string CargoAddress { get; set; }
    public string? ClinicName { get; set; }
    public string? CompanyTitle { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }
}