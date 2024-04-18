using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Organizations.Application.Dto;
using Organizations.Application.Exceptions;
using Organizations.Application.Interfaces;
using Organizations.Application.Utilities;
using Organizations.Domain.Entities;

namespace Organizations.Application.Services;

public class UserService : IUserService
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public UserService(
        IApplicationContext context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetById(Guid userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        return user is null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<Guid> Register(string login, string name, string password)
    {
        var isInvalidLogin = _context.Users.Any(x => x.Login == login);
        if (isInvalidLogin)
            throw new RegistrationException("This login is already taken.");
        
        var passwordHash = Cipher.GetPasswordHash(password);
        var user = new User(login, name, passwordHash);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }
}