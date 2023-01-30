using DentOnline.Application.Constants;
using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.GenerateFileAtFile;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Dtos;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.IntreOrals;
using Microsoft.AspNetCore.Hosting;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Queries.ManipuleIntraOral;

public class
    ManipuleIntraOralQueryRequestHandler : IRequestHandler<ManipuleIntraOralQueryRequest, IDataResponse<IntraOralDto>>
{
    private readonly IWebHostEnvironment _environment;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ManipuleIntraOralQueryRequestHandler(IMapper mapper, IMediator mediator, IWebHostEnvironment environment)
    {
        _mapper = mapper;
        _mediator = mediator;
        _environment = environment;
    }

    public async Task<IDataResponse<IntraOralDto>> Handle(ManipuleIntraOralQueryRequest request,
        CancellationToken cancellationToken)
    {
        var intraOral = request.IntraOral;

        var intraOralDto = _mapper.Map<IntraOral, IntraOralDto>(intraOral);

        var generatedUpperJawResult =
            await _mediator.Send(new GenerateFileAtFileCommandRequest(intraOral.UpperJawVisualFile));
        if (generatedUpperJawResult.IsSuccess)
            intraOralDto.UpperJawVisualFile = generatedUpperJawResult.Data;

        var generatedLowerJawResult =
            await _mediator.Send(new GenerateFileAtFileCommandRequest(intraOral.LowerJawVisualFile));
        if (generatedLowerJawResult.IsSuccess)
            intraOralDto.LowerJawVisualFile = generatedLowerJawResult.Data;

        if (intraOral.CephalometryVisualFile is not null)
        {
            var generatedCephalometryResult =
                await _mediator.Send(new GenerateFileAtFileCommandRequest(intraOral.CephalometryVisualFile));
            if (generatedCephalometryResult.IsSuccess)
                intraOralDto.CephalometryVisualFile = generatedCephalometryResult.Data;
        }

        if (string.IsNullOrEmpty(intraOral.CbctVisualFileName) is false)
        {
            var folderName = FolderConsts.FolderName;

            intraOralDto.CbctVisualFileUrl = string.Join("/", folderName, intraOral.CbctVisualFileName);
        }

        if (intraOral.ClosingScanVisualFile is not null)
        {
            var generatedClosingScanResult =
                await _mediator.Send(new GenerateFileAtFileCommandRequest(intraOral.ClosingScanVisualFile));
            if (generatedClosingScanResult.IsSuccess)
                intraOralDto.ClosingScanVisualFile = generatedClosingScanResult.Data;
        }

        return new SuccessDataResponse<IntraOralDto>(string.Empty, intraOralDto);
    }
}