using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments._Bases.Dtos;
using DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByIdentities;
using DentOnline.Application.Repositories.Treatments._Bases;

namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatments;

public class GetTreatmentsQueryRequestHandler : IRequestHandler<GetTreatmentsQueryRequest,
    IDataResponse<ICollection<TreatmentDto>>>
{
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;
    private readonly ITreatmentReadRepository _readRepository;

    public GetTreatmentsQueryRequestHandler(ITreatmentReadRepository readRepository, IMediator mediator,
        LanguageService languageService)
    {
        _readRepository = readRepository;
        _mediator = mediator;
        _languageService = languageService;
    }

    public async Task<IDataResponse<ICollection<TreatmentDto>>> Handle(
        GetTreatmentsQueryRequest request, CancellationToken cancellationToken)
    {
        var treatments = await _readRepository.GetIdentitiesAsync(request.Pagination);
        if (treatments.Any() is false)
            return new ErrorDataResponse<ICollection<TreatmentDto>>(_languageService.Get(Messages
                .TreatmentsAreNotFound));

        return await _mediator.Send(new GetTreatmentsByIdentitiesQueryRequest(treatments));
    }
}