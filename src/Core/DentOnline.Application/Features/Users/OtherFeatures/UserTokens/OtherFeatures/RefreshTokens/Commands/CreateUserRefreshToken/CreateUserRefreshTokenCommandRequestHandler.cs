using System.Text;
using DentOnline.Application.Constants;
using DentOnline.Application.Repositories.Users;
using DentOnline.Application.Utilities.Exceptions;
using DentOnline.Domain.Concrete.Users._Bases;
using Microsoft.Extensions.Configuration;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.RefreshTokens.Commands.
    CreateUserRefreshToken;

public class
    CreateUserRefreshTokenCommandRequestHandler : IRequestHandler<CreateUserRefreshTokenCommandRequest,
        IDataResponse<CreateUserRefreshTokenCommandResponse>>
{
    private readonly IConfiguration _configuration;
    private readonly LanguageService _languageService;
    private readonly Random _random;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;

    public CreateUserRefreshTokenCommandRequestHandler(Random random, IConfiguration configuration,
        IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository,
        LanguageService languageService)
    {
        _random = random;
        _configuration = configuration;
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _languageService = languageService;
    }

    public async Task<IDataResponse<CreateUserRefreshTokenCommandResponse>> Handle(
        CreateUserRefreshTokenCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = await GetUserFromRequestAsync(request);

        int refreshTokenLength = Convert.ToInt16(_configuration["Token:RefreshTokenCharLength"]);

        StringBuilder sb = new();

        for (var i = 0; i < refreshTokenLength; i++)
        {
            int generatedValue;

            // Generate number
            if (GetRandomBoolean())
                generatedValue = GetNumber();
            else // Generate char
                generatedValue = GetRandomBoolean() ? GetUpperCase() : GetLowerCase();

            sb.Append((char)generatedValue);
        }

        var expiryDate =
            DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Token:RefreshTokenExpiryAsMinute"]));

        CreateUserRefreshTokenCommandResponse responseModel = new(sb.ToString(), expiryDate);

        user.RefreshToken = responseModel;

        await _userWriteRepository.UpdateAsync(user.Id, user);

        return new SuccessDataResponse<CreateUserRefreshTokenCommandResponse>(
            _languageService.Get(Messages.RefreshTokenIsGenerated),
            responseModel);
    }

    private async Task<User> GetUserFromRequestAsync(CreateUserRefreshTokenCommandRequest request)
    {
        var user = request.User;

        if (request.UserId is not null)
            user = await _userReadRepository.GetAsync(_user => request.UserId.Equals(_user.Id));

        if (user is null)
            throw new ErrorException(_languageService.Get(Messages.UserIsNotFound));

        return user;
    }

    private bool GetRandomBoolean()
    {
        return _random.Next(0, 2) is 0;
    }

    private int GetNumber()
    {
        return _random.Next(48, 57);
    }

    private int GetLowerCase()
    {
        return _random.Next(97, 122);
    }

    private int GetUpperCase()
    {
        return _random.Next(65, 90);
    }
}