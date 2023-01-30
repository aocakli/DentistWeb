using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DentOnline.Application.Utilities.Auth;

public class AuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserId()
    {
        return _httpContextAccessor.HttpContext.User.Claims
            ?.FirstOrDefault(_claim => _claim.Type.Equals(ClaimTypes.NameIdentifier))
            ?.Value ?? null;
    }
}