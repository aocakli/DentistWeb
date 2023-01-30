using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments._Bases.Dtos;
using DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByIdentities;
using DentOnline.Application.Repositories.Treatments._Bases;
using DentOnline.Application.Utilities.Auth;

namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByDoctorId;

public class
    GetTreatmentsByDoctorIdQueryRequestHandler : IRequestHandler<GetTreatmentsByDoctorIdQueryRequest,
        IDataResponse<ICollection<TreatmentDto>>>
{
    private readonly AuthService _authService;
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;
    private readonly ITreatmentReadRepository _readRepository;

    public GetTreatmentsByDoctorIdQueryRequestHandler(ITreatmentReadRepository readRepository, IMediator mediator,
        LanguageService languageService, AuthService authService)
    {
        _readRepository = readRepository;
        _mediator = mediator;
        _languageService = languageService;
        _authService = authService;
    }

    public async Task<IDataResponse<ICollection<TreatmentDto>>> Handle(GetTreatmentsByDoctorIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        var userIdOfSendedRequest = _authService.GetUserId();
        if (userIdOfSendedRequest is null || request.DoctorId.Equals(userIdOfSendedRequest) is false)
            return new ErrorDataResponse<ICollection<TreatmentDto>>(_languageService.Get(Messages.AccessDenied));

        var treatmentIdentities =
            await _readRepository.GetIdentitiesAsync(_treatment => request.DoctorId.Equals(_treatment.UserIdOfDoctor));
        if (treatmentIdentities.Any() is false)
            return new ErrorDataResponse<ICollection<TreatmentDto>>(
                _languageService.Get(Messages.TreatmentsAreNotFound));

        return await _mediator.Send(new GetTreatmentsByIdentitiesQueryRequest(treatmentIdentities));
    }
}