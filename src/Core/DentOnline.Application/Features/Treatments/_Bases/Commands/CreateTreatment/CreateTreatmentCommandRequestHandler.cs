using DentOnline.Application.Constants;
using DentOnline.Application.Features.Notifications.Abstracts;
using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;
using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    CreateSickPeopleInformation;
using DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.CreateTreatmentStatu;
using DentOnline.Application.Repositories.Treatments._Bases;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Domain.Concrete.Treatments._Bases;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.TreatmentStatus;

namespace DentOnline.Application.Features.Treatments._Bases.Commands.CreateTreatment;

public class
    CreateTreatmentCommandRequestHandler : IRequestHandler<CreateTreatmentCommandRequest, IDataResponse<Treatment>>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationService _notificationService;
    private readonly ITreatmentWriteRepository _writeRepository;

    public CreateTreatmentCommandRequestHandler(ITreatmentWriteRepository writeRepository, IMapper mapper,
        IMediator mediator, INotificationService notificationService, LanguageService languageService)
    {
        _writeRepository = writeRepository;
        _mapper = mapper;
        _mediator = mediator;
        _notificationService = notificationService;
        _languageService = languageService;
    }

    public async Task<IDataResponse<Treatment>> Handle(CreateTreatmentCommandRequest request,
        CancellationToken cancellationToken)
    {
        var documentToAdd = _mapper.Map<CreateTreatmentCommandRequest, Treatment>(request);

        var createdIntraOralResult = await _mediator.Send(request.CreateIntraOralCommand);
        if (createdIntraOralResult.IsSuccess is false)
            throw new ErrorException(createdIntraOralResult.Message);

        documentToAdd.IntraOral = createdIntraOralResult.Data;

        // İnceleme statüsü eklenir.
        var treatmentStatuResult =
            await CreateTreatmentStatuAndMapToDocumentAsync(TreatmentStatuTypes.ReviewPhase, documentToAdd);
        if (treatmentStatuResult.IsSuccess is false)
            return new ErrorDataResponse<Treatment>(treatmentStatuResult.Message);

        // İncelemeye ilk yorum eklenir.
        var commentResult = await CreateCommentAndMapToDocumentAsync(request.CreateCommentCommand, documentToAdd);
        if (commentResult.IsSuccess is false)
            return new ErrorDataResponse<Treatment>(commentResult.Message);

        // Hastanın bilgileri eklenir.
        var sickPeopleInfoResult =
            await CreateSickPeopleInformationAndMapToDocumentAsync(request.CreateSickPeopleInformationCommand,
                documentToAdd);
        if (sickPeopleInfoResult.IsSuccess is false)
            return new ErrorDataResponse<Treatment>(sickPeopleInfoResult.Message);

        await _writeRepository.CreateAsync(documentToAdd);

#if (!DEBUG)
        var notificationResult =
            await _notificationService.SendNotificationWhenCreatedATreatmentAsync(documentToAdd.Id);
        if (notificationResult.IsSuccess is false)
        {
            string errorMessage = string.Join(" ", _languageService.Get(Messages.TreatmentIsCreated),
                notificationResult.Message);
            return new ErrorDataResponse<Treatment>(errorMessage);
        }
#endif

        if (await _writeRepository.SaveChangesAsync() is false)
            throw new ErrorException(_languageService.Get(Messages.TreatmentIsNotCreated));

        return new SuccessDataResponse<Treatment>(_languageService.Get(Messages.TreatmentIsCreated), documentToAdd);
    }

    private async Task<IResponse> CreateCommentAndMapToDocumentAsync(CreateCommentCommandRequest request,
        Treatment document)
    {
        var createdCommentResult = await _mediator.Send(request);
        if (createdCommentResult.IsSuccess is false)
            return new ErrorResponse(createdCommentResult.Message);

        document.Comments = new List<Comment> { createdCommentResult.Data };

        return new SuccessResponse(createdCommentResult.Message);
    }

    private async Task<IResponse> CreateSickPeopleInformationAndMapToDocumentAsync(
        CreateSickPeopleInformationCommandRequest request,
        Treatment document)
    {
        var createdSickPeopleInformationResult = await _mediator.Send(request);
        if (createdSickPeopleInformationResult.IsSuccess is false)
            return new ErrorResponse(createdSickPeopleInformationResult.Message);

        document.SickPeopleInformation = createdSickPeopleInformationResult.Data;

        return new SuccessResponse(createdSickPeopleInformationResult.Message);
    }

    private async Task<IResponse> CreateTreatmentStatuAndMapToDocumentAsync(TreatmentStatuTypes statuType,
        Treatment document)
    {
        var createdTreatmentStatuResult =
            await _mediator.Send(new CreateTreatmentStatuCommandRequest(statuType));
        if (createdTreatmentStatuResult.IsSuccess is false)
            return new ErrorResponse(createdTreatmentStatuResult.Message);

        document.TreatmentStatuTimeLines = new List<TreatmentStatu> { createdTreatmentStatuResult.Data };

        return new SuccessResponse(createdTreatmentStatuResult.Message);
    }
}