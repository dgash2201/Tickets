using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Clients;
using Web.Interfaces;

namespace Web.Pages.Auth;

public class RegisterOrganizationModel : PageModel
{
    private readonly OrganizationsClient _organizationsClient;
    private readonly ITokenService _tokenService;
    
    [BindProperty]
    [Required]
    public string Login { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [BindProperty]
    [Required]
    [RegularExpression(@"^(([0-9]{12})|([0-9]{10}))?$", ErrorMessage = "Invalid value. INN must contain 10 or 12 digits.")]
    public string Inn { get; set; } = string.Empty;
    
    [BindProperty]
    [Required]
    [RegularExpression(@"^([0-9]{13})?$", ErrorMessage = "Invalid value. OGRN must contain 13 digits")]
    public string Ogrn { get; set; } = string.Empty;
    
    [BindProperty]
    [Required]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string ErrorMessage = string.Empty;

    public RegisterOrganizationModel(OrganizationsClient organizationsClient, ITokenService tokenService)
    {
        _organizationsClient = organizationsClient;
        _tokenService = tokenService;
    }

    public IActionResult OnGet()
    {
        if (_tokenService.IsAuthenticated())
            return RedirectToPage("/MyAccount/Index");

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (_tokenService.IsAuthenticated())
            return RedirectToPage("/MyAccount/Index");
        
        if (!ModelState.IsValid)
            return Page();
        
        var response = await _organizationsClient.RegisterAsync(Login, Name, Password, Inn, Ogrn);

        if (response.IsSuccessStatusCode)
            return RedirectToPage("/Auth/Login");

        ErrorMessage = response.StatusCode == System.Net.HttpStatusCode.Conflict
            ? await response.Content.ReadAsStringAsync()
            : "An error occurred while processing your request.";

        return Page();
    }
}