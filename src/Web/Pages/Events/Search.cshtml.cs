using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Clients;
using Web.Dto;

namespace Web.Pages.Events
{
    public class Search : PageModel
    {
        private readonly EventsClient _eventsClient;

        public Search(EventsClient eventsClient)
        {
            _eventsClient = eventsClient;
        }

        public IEnumerable<EventDto>? Events;
        
        public async Task OnGet(string? search)
        {
            Events = await _eventsClient.SearchAsync(search);
        }
    }
}