using System.Text;
using DentOnline.Application.Constants;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.GenerateUserVerification;

public class
    GenerateUserVerificationCommandRequestHandler : IRequestHandler<GenerateUserVerificationCommandRequest,
        IDataResponse<UserVerification>>
{
    private const byte VerificationCodeLength = 100;
    private const byte VerificationCodeExpiryDateAsHour = 2;
    private readonly LanguageService _languageService;

    private readonly IMapper _mapper;
    private readonly Random _random;

    public GenerateUserVerificationCommandRequestHandler(IMapper mapper, Random random, LanguageService languageService)
    {
        _mapper = mapper;
        _random = random;
        _languageService = languageService;
    }

    public async Task<IDataResponse<UserVerification>> Handle(GenerateUserVerificationCommandRequest request,
        CancellationToken cancellationToken)
    {
        var userVerification = _mapper.Map<GenerateUserVerificationCommandRequest, UserVerification>(request);

        StringBuilder sb = new();

        for (var i = 1; i <= VerificationCodeLength; i++)
        {
            var generatedChar = (char)_random.Next(65, 90);
            sb.Append(generatedChar);
        }

        userVerification.Code = sb.ToString();

        userVerification.ExpiryDate = DateTime.UtcNow.AddHours(VerificationCodeExpiryDateAsHour);

        return new SuccessDataResponse<UserVerification>(_languageService.Get(Messages.UserVerificationIsGenerated),
            userVerification);
    }
}