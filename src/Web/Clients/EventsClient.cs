using System.Text;
using Newtonsoft.Json;
using Web.Dto;

namespace Web.Clients
{
    public class EventsClient
    {
        private readonly HttpClient _httpClient;

        public EventsClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["EventsBaseAddress"]!);
        }

        public async Task<IEnumerable<EventDto>?> SearchAsync(string? search)
        {
            var sb = new StringBuilder();
            sb.Append("search?");
            if (!string.IsNullOrWhiteSpace(search))
            {
                sb.Append($"search={search}");
            }
            var response = await _httpClient.GetAsync(sb.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<EventDto>>(content);
        }

        public async Task<EventDto?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<EventDto>(content);
        } 
        
        public async Task<IEnumerable<EventDto>?> GetFutureEventsAsync(Guid organizationId)
        {
            var url = $"future?organizationId={organizationId}";
            
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<EventDto>>(content);
        }
        
        public async Task<IEnumerable<EventDto>?> GetByOrganizationIdAsync(Guid organizationId)
        {
            var url = $"?organizationId={organizationId}";
            
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<EventDto>>(content);
        }

        public async Task Create(CreateEventDto dto)
        {
            var request = new
            {
                title = dto.Title,
                description = dto.Description,
                organizationId = dto.OrganizationId,
                date = dto.Date,
                imageName = dto.File.FileName
            };
            await _httpClient.PostAsJsonAsync(string.Empty, request);
        }
    }
}