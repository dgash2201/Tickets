using Microsoft.AspNetCore.Mvc;
using Organizations.Api.Models.Requests;
using Organizations.Application.Dto;
using Organizations.Application.Exceptions;
using Organizations.Application.Interfaces;

namespace Organizations.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid userId)
    {
        var user = await _userService.GetById(userId);
        
        return user is null ? NotFound() : Ok(user);
    }
    
    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<Guid>> Register(RegisterUserRequestModel model)
    {
        try
        {
            var userId = await _userService.Register(model.Login, model.Name, model.Password);

            return Ok(userId);
        }
        catch (RegistrationException e)
        {
            return Conflict(e.Message);
        }
    }
}