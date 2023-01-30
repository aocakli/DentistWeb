using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments._Bases.Dtos;
using DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByIdentities;
using DentOnline.Application.Features.Users._Bases.Queries.GetRolesByUserId;
using DentOnline.Application.Utilities.Auth;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;

namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentById;

public class
    GetTreatmentByIdQueryRequestHandler : IRequestHandler<GetTreatmentByIdQueryRequest, IDataResponse<TreatmentDto>>
{
    private readonly AuthService _authService;
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;

    public GetTreatmentByIdQueryRequestHandler(IMediator mediator, LanguageService languageService,
        AuthService authService)
    {
        _mediator = mediator;
        _languageService = languageService;
        _authService = authService;
    }

    public async Task<IDataResponse<TreatmentDto>> Handle(GetTreatmentByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        var treatmentResult = await _mediator.Send(new GetTreatmentsByIdentitiesQueryRequest(request.Id));
        if (treatmentResult.IsSuccess is false)
            return new ErrorDataResponse<TreatmentDto>(treatmentResult.Message);

        if (treatmentResult.Data.FirstOrDefault() is null)
            return new ErrorDataResponse<TreatmentDto>(_languageService.Get(Messages.TreatmentIsNotFound));

        await CheckRequestOwnerAsync(treatmentResult.Data.First());

        return new SuccessDataResponse<TreatmentDto>(_languageService.Get(Messages.TreatmentIsBrought),
            treatmentResult.Data.First());
    }

    private async Task CheckRequestOwnerAsync(TreatmentDto treatment)
    {
        var requestOwnerUserId = _authService.GetUserId();

        var doctorId = treatment.Doctor.Id;

        if (requestOwnerUserId.Equals(doctorId) is false)
        {
            var requestOwnerResult = await _mediator.Send(new GetRolesByUserIdQueryRequest(requestOwnerUserId));
            if (requestOwnerResult.IsSuccess is false)
                throw new ErrorException(requestOwnerResult.Message);

            if (requestOwnerResult.Data.Any(_role => _role == UserRoles.Admin) is false)
                throw new BusinessException(_languageService.Get(Messages.AccessDenied));
        }
    }
}