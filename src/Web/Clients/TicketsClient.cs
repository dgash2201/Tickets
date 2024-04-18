using Newtonsoft.Json;
using Web.Dto;

namespace Web.Clients
{
    public class TicketsClient
    {
        private readonly HttpClient _httpClient;

        public TicketsClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["TicketsBaseAddress"]!);
        }

        public async Task<IEnumerable<TicketTypeDto>> GetTicketTypes(Guid eventId)
        {
            var response = await _httpClient.GetAsync($"byEvent/{eventId}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<TicketTypeDto>>(content) ?? Enumerable.Empty<TicketTypeDto>();
        }

        public async Task<TicketTypeDto?> GetTicketType(Guid id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TicketTypeDto>(content);
        }
    }
}