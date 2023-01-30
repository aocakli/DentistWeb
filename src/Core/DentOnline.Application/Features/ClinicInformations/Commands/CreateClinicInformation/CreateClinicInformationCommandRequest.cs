using System.Text.Json.Serialization;
using DentOnline.Domain.Concrete.ClinicInformations;

namespace DentOnline.Application.Features.ClinicInformations.Commands.CreateClinicInformation;

public class CreateClinicInformationCommandRequest : IRequest<IDataResponse<ClinicInformation>>
{
    [JsonIgnore] public string UserId { get; set; } = string.Empty;
    public string TelephoneNumber { get; set; }
    public string CargoAddress { get; set; }
    public string? ClinicName { get; set; }
    public string? CompanyTitle { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }

    [JsonIgnore] public bool IsSaveChanges { get; set; }
}