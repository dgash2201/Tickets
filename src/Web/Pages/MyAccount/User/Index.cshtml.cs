using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;

namespace Web.Pages.MyAccount.User;

public class IndexModel : PageModel
{
    private readonly ITokenService _tokenService;

    public IndexModel(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public IActionResult OnGet()
    {
        if (!_tokenService.IsAuthenticated())
            return RedirectToPage("/Auth/Login");
        
        if (_tokenService.IsOrganization())
            return RedirectToPage("/MyAccount/Organization/Index");

        return Page();
    }
}