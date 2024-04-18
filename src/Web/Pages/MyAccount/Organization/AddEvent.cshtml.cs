using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Dto;
using Web.Interfaces;

namespace Web.Pages.MyAccount.Organization
{
    public class AddEvent : PageModel
    {
        private readonly ITokenService _tokenService;
        private readonly IEventService _eventService;

        public AddEvent(ITokenService tokenService, IEventService eventService)
        {
            _tokenService = tokenService;
            _eventService = eventService;
        }
        
        public Guid OrganizationId { get; set; }
        
        public IActionResult OnGet()
        {
            if (!_tokenService.IsAuthenticated())
                return RedirectToPage("/Auth/Login");

            if (_tokenService.IsUser())
                return RedirectToPage("/MyAccount/User/Index");
        
            OrganizationId = Guid.Parse(_tokenService.GetUserId()!);
        
            return Page();
        }

        public async Task<IActionResult> OnPost(CreateEventDto dto)
        {
            await _eventService.Create(dto);

            return RedirectToPage("/MyAccount/Organization/Events");
        }
    }
}