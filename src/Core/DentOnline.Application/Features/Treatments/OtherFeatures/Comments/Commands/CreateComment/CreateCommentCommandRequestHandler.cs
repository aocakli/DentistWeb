using DentOnline.Application.Constants;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.Comments;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateComment;

public class CreateCommentCommandRequestHandler : IRequestHandler<CreateCommentCommandRequest, IDataResponse<Comment>>
{
    private readonly LanguageService _languageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateCommentCommandRequestHandler(IMapper mapper, IMediator mediator, LanguageService languageService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _languageService = languageService;
    }

    public async Task<IDataResponse<Comment>> Handle(CreateCommentCommandRequest request,
        CancellationToken cancellationToken)
    {
        var documentToAdd = _mapper.Map<CreateCommentCommandRequest, Comment>(request);

        if (request.CreateFilesCommand is not null)
        {
            var createFilesResult = await _mediator.Send(request.CreateFilesCommand);
            if (createFilesResult.IsSuccess is false)
                return new ErrorDataResponse<Comment>(createFilesResult.Message);

            documentToAdd.Files = createFilesResult.Data;
        }

        return new SuccessDataResponse<Comment>(_languageService.Get(Messages.CommentIsCreated), documentToAdd);
    }
}