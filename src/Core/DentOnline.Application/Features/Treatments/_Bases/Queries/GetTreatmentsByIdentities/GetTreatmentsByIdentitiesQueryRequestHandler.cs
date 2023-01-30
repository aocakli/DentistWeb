using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments._Bases.Dtos;
using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Dtos;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.GenerateFileAtFile;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Dtos;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Queries.ManipuleIntraOral;
using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Queries.
    ManipuleSickPeopleInformation;
using DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Queries.
    GetAvailableTreatmentStatusByStatu;
using DentOnline.Application.Features.Users._Bases.Queries.GetUsersByIdentities;
using DentOnline.Application.Repositories.Treatments._Bases;
using DentOnline.Domain.Concrete.Treatments._Bases;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments;
using File = DentOnline.Domain.Concrete.Treatments.OtherDomains.Files.File;

namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByIdentities;

public class GetTreatmentsByIdentitiesQueryRequestHandler : IRequestHandler<GetTreatmentsByIdentitiesQueryRequest,
    IDataResponse<ICollection<TreatmentDto>>>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITreatmentReadRepository _readRepository;


    public GetTreatmentsByIdentitiesQueryRequestHandler(ITreatmentReadRepository readRepository, IMapper mapper,
        IMediator mediator, LanguageService languageService)
    {
        _readRepository = readRepository;
        _mapper = mapper;
        _mediator = mediator;
        _languageService = languageService;
    }

    public async Task<IDataResponse<ICollection<TreatmentDto>>> Handle(
        GetTreatmentsByIdentitiesQueryRequest request, CancellationToken cancellationToken)
    {
        var treatments = await _readRepository.GetAllAsync(_treatment => request.Identities.Contains(_treatment.Id));
        if (treatments.Any() is false)
            return new ErrorDataResponse<ICollection<TreatmentDto>>(_languageService.Get(Messages
                .TreatmentsAreNotFound));

        // Yorum atan kullanıcıların idleri.
        var userIdentitiesOfCommentOwner = treatments
            .SelectMany(_treatment => _treatment.Comments.Select(_comment => _comment.UserId))
            .ToHashSet();

        var userIdentitiesOfDoctor = treatments.Select(_treatment => _treatment.UserIdOfDoctor).ToHashSet();

        var userIdentities = userIdentitiesOfCommentOwner.Concat(userIdentitiesOfDoctor).ToHashSet();

        var usersResult =
            await _mediator.Send(new GetUsersByIdentitiesQueryRequest(userIdentities));
        if (usersResult.IsSuccess is false)
            return new ErrorDataResponse<ICollection<TreatmentDto>>(usersResult.Message);

        List<TreatmentDto> responseModel = new();

        foreach (var treatment in treatments)
        {
            var treatmentDto = _mapper.Map<Treatment, TreatmentDto>(treatment);

            treatmentDto.Doctor = usersResult.Data.FirstOrDefault(_user => _user.Id.Equals(treatment.UserIdOfDoctor));

            var manipulatedIntraOralResult =
                await _mediator.Send(new ManipuleIntraOralQueryRequest(treatment.IntraOral));
            if (manipulatedIntraOralResult.IsSuccess)
                treatmentDto.IntraOral = manipulatedIntraOralResult.Data;

            var manipulatedSickPeopleInformationResult =
                await _mediator.Send(new ManipuleSickPeopleInformationQueryRequest(treatment.SickPeopleInformation));
            if (manipulatedSickPeopleInformationResult.IsSuccess)
                treatmentDto.SickPeopleInformation = manipulatedSickPeopleInformationResult.Data;

            foreach (var comment in treatment.Comments)
            {
                var commentDto = _mapper.Map<Comment, CommentDto>(comment);

                commentDto.User = usersResult.Data.FirstOrDefault(_user => _user.Id.Equals(comment.UserId));

                if (comment.Files is not null && comment.Files.Any())
                    await ExportFileInComment(commentDto, comment.Files);

                (treatmentDto.Comments ??= new List<CommentDto>()).Add(commentDto);
            }

            var availableTreatmentStatusResult = await _mediator.Send(
                new GetAvailableTreatmentStatusByStatuQueryRequest(treatmentDto.TreatmentStatuTimeLines
                    .Last().TreatmentStatuTypes));
            if (availableTreatmentStatusResult.IsSuccess)
                treatmentDto.AvailableTreatmentStatus = availableTreatmentStatusResult.Data;

            responseModel.Add(treatmentDto);
        }

        return new SuccessDataResponse<ICollection<TreatmentDto>>(
            _languageService.Get(Messages.TreatmentsAreListed), responseModel);
    }

    private async Task ExportFileInComment(CommentDto commentDto,
        ICollection<File> files)
    {
        if (commentDto.Files is null)
            commentDto.Files = new List<FileDto>();

        foreach (var file in files)
        {
            var generatedFileAtFileResult = await _mediator.Send(new GenerateFileAtFileCommandRequest(file));
            if (generatedFileAtFileResult.IsSuccess is false)
                return;

            commentDto.Files.Add(generatedFileAtFileResult.Data);
        }
    }
}