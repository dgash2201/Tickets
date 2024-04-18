using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;

namespace Web.Pages.MyAccount;

public class IndexModel : PageModel
{
    private readonly ITokenService _tokenService;

    public IndexModel(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public IActionResult OnGet()
    {
        return _tokenService.IsAuthenticated()
            ? RedirectToPage(_tokenService.IsUser() ? "/MyAccount/User/Index" : "/MyAccount/Organization/Index")
            : RedirectToPage("/Auth/Login");
    }
}