using DentOnline.Application.Constants;
using DentOnline.Application.Features.Notifications.Abstracts;
using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;
using DentOnline.Application.Features.Users._Bases.Queries.GetUserById;
using DentOnline.Application.Repositories.Treatments._Bases;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateCommentToTreatment;

public class
    CreateCommentToTreatmentCommandRequestHandler : IRequestHandler<CreateCommentToTreatmentCommandRequest, IResponse>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationService _notificationService;
    private readonly ITreatmentReadRepository _treatmentReadRepository;
    private readonly ITreatmentWriteRepository _treatmentWriteRepository;

    public CreateCommentToTreatmentCommandRequestHandler(IMediator mediator,
        ITreatmentReadRepository treatmentReadRepository, ITreatmentWriteRepository treatmentWriteRepository,
        IMapper mapper, LanguageService languageService, INotificationService notificationService)
    {
        _mediator = mediator;
        _treatmentReadRepository = treatmentReadRepository;
        _treatmentWriteRepository = treatmentWriteRepository;
        _mapper = mapper;
        _languageService = languageService;
        _notificationService = notificationService;
    }

    public async Task<IResponse> Handle(CreateCommentToTreatmentCommandRequest request,
        CancellationToken cancellationToken)
    {
        var treatment =
            await _treatmentReadRepository.GetAsync(_treatment => _treatment.Id.Equals(request.TreatmentId));
        if (treatment is null)
            return new ErrorResponse(_languageService.Get(Messages.TreatmentIsNotFound));

        var createCommentRequest =
            _mapper.Map<CreateCommentToTreatmentCommandRequest, CreateCommentCommandRequest>(request);

        // Treatment'e eklenecek yorum için document üretir.
        var commentsResult = await _mediator.Send(createCommentRequest);
        if (commentsResult.IsSuccess is false)
            return new ErrorResponse(commentsResult.Message);

        // Yorumu atan kullanıcıyı çeken ve kontrol eder.
        var userResult = await _mediator.Send(new GetUserByIdQueryRequest(request.UserId));
        if (userResult.IsSuccess is false)
            return new ErrorResponse(userResult.Message);

        if (treatment.Comments is null)
            treatment.Comments = new List<Comment>();

        treatment.Comments.Add(commentsResult.Data);

        await _treatmentWriteRepository.UpdateAsync(treatment.Id, treatment);

        // await CreateTreatmentStatuAndMapToDocument(userResult, treatment);

        if (await _treatmentWriteRepository.SaveChangesAsync() is false)
            throw new ErrorException(_languageService.Get(Messages.TreatmentIsNotCreated));

#if (!DEBUG)
        await _notificationService.SendNotificationWhenAddedACommentToTreatmentAsync(request.TreatmentId);
#endif

        return new SuccessResponse(_languageService.Get(Messages.CommentIsAddedToTreatment));
    }
}