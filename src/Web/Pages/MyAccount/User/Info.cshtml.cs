using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Clients;
using Web.Dto;
using Web.Interfaces;

namespace Web.Pages.MyAccount.User;

public class InfoModel : PageModel
{
    private readonly ITokenService _tokenService;
    private readonly UsersClient _usersClient;

    public InfoModel(ITokenService tokenService, UsersClient usersClient)
    {
        _tokenService = tokenService;
        _usersClient = usersClient;
    }
    
    [BindProperty]
    public UserDto? UserDto { get; set; }

    public async Task<IActionResult> OnGet()
    {
        if (!_tokenService.IsAuthenticated())
            return RedirectToPage("/Auth/Login");

        if (_tokenService.IsOrganization())
            return RedirectToPage("/MyAccount/Organization/Index");
        
        var userId = _tokenService.GetUserId();
        var user = await _usersClient.GetByIdAsync(new Guid(userId!));
        UserDto = user;

        return Page();
    }
}