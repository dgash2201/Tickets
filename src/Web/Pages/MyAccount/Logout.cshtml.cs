using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;

namespace Web.Pages.MyAccount;

public class LogoutModel : PageModel
{
    private readonly ITokenService _tokenService;

    public LogoutModel(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public IActionResult OnGet()
    {
        _tokenService.RemoveToken();

        return RedirectToPage("/Auth/Login");
    }
}