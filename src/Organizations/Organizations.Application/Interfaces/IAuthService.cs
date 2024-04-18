namespace Organizations.Application.Interfaces;

public interface IAuthService
{
    Task<string> GetToken(string login, string password);
}