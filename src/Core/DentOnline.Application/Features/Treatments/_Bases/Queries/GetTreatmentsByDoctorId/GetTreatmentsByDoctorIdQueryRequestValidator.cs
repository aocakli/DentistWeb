namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByDoctorId;

public class GetTreatmentsByDoctorIdQueryRequestValidator : AbstractValidator<GetTreatmentsByDoctorIdQueryRequest>
{
    public GetTreatmentsByDoctorIdQueryRequestValidator()
    {
        RuleFor(x => x.DoctorId).NotNull().NotEmpty();
    }
}