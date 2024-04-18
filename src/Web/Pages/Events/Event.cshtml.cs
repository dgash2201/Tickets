using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Clients;
using Web.Dto;
using Web.Interfaces;

namespace Web.Pages.Events
{
    public class Event : PageModel
    {
        private readonly EventsClient _eventsClient;
        private readonly TicketsClient _ticketsClient;
        private readonly PaymentsClient _paymentsClient;
        private readonly ITokenService _tokenService;

        public Event(EventsClient eventsClient, TicketsClient ticketsClient, PaymentsClient paymentsClient, ITokenService tokenService)
        {
            _eventsClient = eventsClient;
            _ticketsClient = ticketsClient;
            _paymentsClient = paymentsClient;
            _tokenService = tokenService;
        }

        public EventDto? EventDto;
        
        public IEnumerable<TicketTypeDto> TicketTypes { get; set; }
        
        public async Task OnGet(Guid id)
        {
            EventDto = await _eventsClient.GetByIdAsync(id);
            TicketTypes = await _ticketsClient.GetTicketTypes(id);
        }

        public async Task<IActionResult> OnPost(BuyTicketDto dto)
        {
            var userId = _tokenService.GetUserId()!;
            await _paymentsClient.Buy(dto.TypeId, Guid.Parse(userId));
            
            return RedirectToPage("/MyAccount/User/Tickets");
        }
    }
}