using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Organizations.Api.Models.Requests;
using Organizations.Application.Interfaces;

namespace Organizations.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<string>> Login(GetTokenRequestModel model)
    {
        try
        {
            var token = await _authService.GetToken(model.Login, model.Password);

            return Ok(token);
        }
        catch (AuthenticationException e)
        {
            return Unauthorized(e.Message);
        }
    }
}