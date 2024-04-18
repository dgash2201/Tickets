using System.Net.Http.Json;
using Dos.Models;

namespace Dos.HttpClients;

public class TicketsClient
{
    private readonly HttpClient _httpClient;

    public TicketsClient(HttpClient httpClient) => _httpClient = httpClient;

    public async ValueTask<Guid> CreateTicketTypeAsync(int maxCount, CancellationToken cancellationToken = default)
    {
        const string url = "api/Tickets/Create";
        const decimal price = 0.01m;

        using var content = JsonContent.Create(new CreateTicketTypeModel
        {
            EventId = Guid.NewGuid(),
            Title = Guid.NewGuid().ToString(),
            Description = string.Empty,
            Price = price,
            MaxCount = maxCount,
            SalesStartDate = DateTime.UtcNow.AddDays(-1),
            SalesEndDate = DateTime.UtcNow.AddDays(1)
        });
        using var response = await _httpClient.PostAsync(url, content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var contentString = await response.Content.ReadAsStringAsync(cancellationToken);
        var id = Guid.Parse(contentString.Trim('"'));

        return id;
    }
}

