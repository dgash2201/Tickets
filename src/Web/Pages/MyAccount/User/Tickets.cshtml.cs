using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Clients;
using Web.Dto;
using Web.Interfaces;

namespace Web.Pages.MyAccount.User;

public class TicketsModel : PageModel
{
    private readonly ITokenService _tokenService;
    private readonly PaymentsClient _paymentsClient;
    private readonly EventsClient _eventsClient;
    private readonly TicketsClient _ticketsClient;
    private readonly OrganizationsClient _organizationsClient;

    public TicketsModel(ITokenService tokenService, PaymentsClient paymentsClient, EventsClient eventsClient, TicketsClient ticketsClient, OrganizationsClient organizationsClient)
    {
        _tokenService = tokenService;
        _paymentsClient = paymentsClient;
        _eventsClient = eventsClient;
        _ticketsClient = ticketsClient;
        _organizationsClient = organizationsClient;
    }

    [BindProperty] 
    public List<MyPaymentDto> MyPayments { get; set; } = new List<MyPaymentDto>();

    public async Task<IActionResult> OnGet()
    {
        if (!_tokenService.IsAuthenticated())
            return RedirectToPage("/Auth/Login");
        
        if (_tokenService.IsOrganization())
            return RedirectToPage("/MyAccount/Organization/Index");
        
        var payments = await _paymentsClient.GetByUserId(Guid.Parse(_tokenService.GetUserId()!));

        foreach (var payment in payments)
        {
            var ticketType = await _ticketsClient.GetTicketType(payment.TicketTypeId);
            var _event = await _eventsClient.GetByIdAsync(ticketType!.EventId);
            var organization = await _organizationsClient.GetByIdAsync(_event!.OrganizationId);
            
            MyPayments.Add(new MyPaymentDto
            {
                Payment = payment,
                Event = _event,
                Organization = organization!,
                TicketType = ticketType
            });
        }

        
        
        return Page();
    }
}