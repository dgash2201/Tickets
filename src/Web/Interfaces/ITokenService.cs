namespace Web.Interfaces;

public interface ITokenService
{
    void SaveToken(string token);
    string? GetToken();
    void RemoveToken();
    bool IsAuthenticated();
    string? GetUserId();
    string? GetUserName();
    string? GetUserType();
    bool IsUser();
    bool IsOrganization();
}