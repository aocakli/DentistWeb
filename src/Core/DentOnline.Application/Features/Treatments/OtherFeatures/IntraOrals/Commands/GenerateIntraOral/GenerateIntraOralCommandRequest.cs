using DentOnline.Application.Features.Treatments.OtherFeatures.Files.Commands.CreateFiles;
using DentOnline.Domain.Concrete.Treatments.OtherDomains.IntreOrals;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.GenerateIntraOral;

public class GenerateIntraOralCommandRequest : IRequest<IDataResponse<IntraOral>>
{
    // public GenerateIntraOralCommandRequest(CreateFilesCommandRequest createUpperJawVisualFileCommand,
    //     CreateFilesCommandRequest createLowerJawVisualFileCommand,
    //     CreateFilesCommandRequest? createClosingScanVisualFileCommand,
    //     CreateFilesCommandRequest? createCephalometryVisualFileCommand,
    //     CreateFilesCommandRequest? createCbctVisualFileCommand)
    // {
    //     CreateUpperJawVisualFileCommand = createUpperJawVisualFileCommand;
    //     CreateLowerJawVisualFileCommand = createLowerJawVisualFileCommand;
    //     CreateClosingScanVisualFileCommand = createClosingScanVisualFileCommand;
    //     CreateCephalometryVisualFileCommand = createCephalometryVisualFileCommand;
    //     CreateCbctVisualFileCommand = createCbctVisualFileCommand;
    // }
    public CreateFilesCommandRequest CreateUpperJawVisualFileCommand { get; set; }
    public CreateFilesCommandRequest CreateLowerJawVisualFileCommand { get; set; }
    public CreateFilesCommandRequest? CreateClosingScanVisualFileCommand { get; set; }
    public CreateFilesCommandRequest? CreateCephalometryVisualFileCommand { get; set; }
    public CreateFilesCommandRequest? CreateCbctVisualFileCommand { get; set; }
}