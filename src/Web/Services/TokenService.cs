using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web.Interfaces;

namespace Web.Services;

public class TokenService : ITokenService
{
    private const string UserIdClaimName = "user_id";
    private const string UserNameClaimName = "user_name";
    private const string UserTypeClaimName = "user_type";
    private const string TokenKey = "UserJwtToken";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SaveToken(string token)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(TokenKey, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }

    public string? GetToken()
    {
        return _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(TokenKey, out var token) == true
            ? token
            : null;
    }
    
    public void RemoveToken()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(TokenKey);
    }

    public bool IsAuthenticated()
    {
        return !string.IsNullOrEmpty(GetToken());
    }

    public string? GetUserId()
    {
        return GetClaims()?.FirstOrDefault(x => x.Type == UserIdClaimName)?.Value;
    }

    public string? GetUserName()
    {
        return GetClaims()?.FirstOrDefault(x => x.Type == UserNameClaimName)?.Value;
    }
    
    public string? GetUserType()
    {
        return GetClaims()?.FirstOrDefault(x => x.Type == UserTypeClaimName)?.Value;
    }

    public bool IsUser()
    {
        return IsAuthenticated() && GetUserType() == "User";
    }

    public bool IsOrganization()
    {
        return IsAuthenticated() && GetUserType() == "Organization";
    }

    private IEnumerable<Claim>? GetClaims()
    {
        var token = GetToken();
        if (token is null)
            return null;

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        return jsonToken?.Claims;
    }
}