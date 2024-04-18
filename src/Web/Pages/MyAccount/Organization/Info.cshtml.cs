using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Clients;
using Web.Dto;
using Web.Interfaces;

namespace Web.Pages.MyAccount.Organization;

public class InfoModel : PageModel
{
    private readonly ITokenService _tokenService;
    private readonly OrganizationsClient _organizationsClient;

    public InfoModel(ITokenService tokenService, OrganizationsClient organizationsClient)
    {
        _tokenService = tokenService;
        _organizationsClient = organizationsClient;
    }
    
    [BindProperty]
    public OrganizationDto? Organization { get; set; }

    public async Task<IActionResult> OnGet()
    {
        if (!_tokenService.IsAuthenticated())
            return RedirectToPage("/Auth/Login");

        if (_tokenService.IsUser())
            return RedirectToPage("/MyAccount/User/Index");
        
        var organizationId = _tokenService.GetUserId();
        var organization = await _organizationsClient.GetByIdAsync(new Guid(organizationId!));
        Organization = organization;

        return Page();
    }
}