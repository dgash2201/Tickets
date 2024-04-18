using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Clients;
using Web.Interfaces;

namespace Web.Pages.Auth;

public class RegisterUserModel : PageModel
{
    private readonly UsersClient _usersClient;
    private readonly ITokenService _tokenService;
    
    [BindProperty]
    [Required]
    public string Login { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [BindProperty]
    [Required]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string ErrorMessage = string.Empty;

    public RegisterUserModel(UsersClient usersClient, ITokenService tokenService)
    {
        _usersClient = usersClient;
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
        
        var response = await _usersClient.RegisterAsync(Login, Name, Password);

        if (response.IsSuccessStatusCode)
            return RedirectToPage("/Auth/Login");

        ErrorMessage = response.StatusCode == System.Net.HttpStatusCode.Conflict
            ? await response.Content.ReadAsStringAsync()
            : "An error occurred while processing your request.";

        return Page();
    }
}