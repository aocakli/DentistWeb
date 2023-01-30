using DentOnline.Domain.Abstractions;
using DentOnline.Domain.Concrete.ClinicInformations.Abstracts;
using MongoDB.Bson.Serialization.Attributes;

namespace DentOnline.Domain.Concrete.ClinicInformations;

public class ClinicInformation : DocumentBase, IClinicInformationBase
{
    [BsonElement("telephone-number")] public string TelephoneNumber { get; set; }

    [BsonElement("cargo-address")] public string CargoAddress { get; set; }

    [BsonElement("clinic-name")] public string? ClinicName { get; set; }

    [BsonElement("company-title")] public string? CompanyTitle { get; set; }

    [BsonElement("tax-office")] public string? TaxOffice { get; set; }

    [BsonElement("tax-number")] public string? TaxNumber { get; set; }
}