using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Clients;
using Web.Dto;

namespace Web.Pages.Organizations
{
    public class Organization : PageModel
    {
        private readonly OrganizationsClient _organizationsClient;
        private readonly EventsClient _eventsClient;

        public Organization(OrganizationsClient organizationsClient, EventsClient eventsClient)
        {
            _organizationsClient = organizationsClient;
            _eventsClient = eventsClient;
        }
        
        public OrganizationDto? OrganizationDto { get; set; }
        
        public IEnumerable<EventDto>? FutureEvents { get; set; }
        
        public async Task OnGet(Guid id)
        {
            OrganizationDto = await _organizationsClient.GetByIdAsync(id);
            FutureEvents = await _eventsClient.GetFutureEventsAsync(id);
        }
    }
}