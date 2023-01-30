using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.CreateCbctVisualFile;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.IntreOrals;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.GenerateIntraOral;

public class
    GenerateIntraOralCommandRequestHandler : IRequestHandler<GenerateIntraOralCommandRequest, IDataResponse<IntraOral>>
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;

    public GenerateIntraOralCommandRequestHandler(IMediator mediator, LanguageService languageService,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        _mediator = mediator;
        _languageService = languageService;
        _configuration = configuration;
        _environment = environment;
    }

    public async Task<IDataResponse<IntraOral>> Handle(GenerateIntraOralCommandRequest request,
        CancellationToken cancellationToken)
    {
        IntraOral intraOral = new()
        {
            CreatedDate = DateTime.UtcNow
        };

        // Sefalometri yüklenmiyor ise cbct yüklenir.
        if (request.CreateCephalometryVisualFileCommand is not null)
        {
            var createdCephalometryVisualFileResult = await _mediator.Send(request.CreateCephalometryVisualFileCommand);
            if (createdCephalometryVisualFileResult.IsSuccess)
                intraOral.CephalometryVisualFile = createdCephalometryVisualFileResult.Data.FirstOrDefault();
        }
        else if (request.CreateCbctVisualFileCommand is not null)
        {
            var createdCbctVisualFileResult = await _mediator.Send(
                new CreateCbctVisualFileCommandRequest(request.CreateCbctVisualFileCommand.Files.FirstOrDefault()));
            if (createdCbctVisualFileResult.IsSuccess)
                intraOral.CbctVisualFileName = createdCbctVisualFileResult.Data;
        }

        if (request.CreateClosingScanVisualFileCommand is not null)
        {
            var createdClosingScanVisualFileResult = await _mediator.Send(request.CreateClosingScanVisualFileCommand);
            if (createdClosingScanVisualFileResult.IsSuccess)
                intraOral.ClosingScanVisualFile = createdClosingScanVisualFileResult.Data.FirstOrDefault();
        }

        var createdUpperJawVisualFileResult = await _mediator.Send(request.CreateUpperJawVisualFileCommand);
        if (createdUpperJawVisualFileResult.IsSuccess)
            intraOral.UpperJawVisualFile = createdUpperJawVisualFileResult.Data.FirstOrDefault();

        var createdLowerJawVisualFileResult = await _mediator.Send(request.CreateLowerJawVisualFileCommand);
        if (createdLowerJawVisualFileResult.IsSuccess)
            intraOral.LowerJawVisualFile = createdLowerJawVisualFileResult.Data.FirstOrDefault();

        return new SuccessDataResponse<IntraOral>(_languageService.Get(Messages.IntraOralIsGenerated), intraOral);
    }
}