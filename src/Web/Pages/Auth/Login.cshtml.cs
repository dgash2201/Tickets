using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Clients;
using Web.Interfaces;

namespace Web.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly AuthClient _authClient;
    private readonly ITokenService _tokenService;

    public LoginModel(
        AuthClient authClient,
        ITokenService tokenService)
    {
        _authClient = authClient;
        _tokenService = tokenService;
    }

    [BindProperty] 
    [Required]
    public string Login { get; set; } = string.Empty;

    [BindProperty] 
    [Required]
    public string Password { get; set; } = string.Empty;

    public string ErrorMessage = string.Empty;

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
        
        var response = await _authClient.LoginAsync(Login, Password);
            
        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            _tokenService.SaveToken(token);
            
            return RedirectToPage("/Index");
        }

        ErrorMessage = response.StatusCode == System.Net.HttpStatusCode.Unauthorized
            ? await response.Content.ReadAsStringAsync()
            : "An error occurred while processing your request.";

        return Page();
    }
}