using Organizations.Application.Dto;

namespace Organizations.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetById(Guid userId);
    
    Task<Guid> Register(string login, string name, string password);
}