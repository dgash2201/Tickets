using System.Net.Http.Json;
using Dos.Models;

namespace Dos.HttpClients;

public class PaymentsClient
{
    private readonly HttpClient _httpClient;

    public PaymentsClient(HttpClient httpClient) => _httpClient = httpClient;

    public async ValueTask<bool> BuyAsync(Guid ticketType, CancellationToken cancellationToken = default)
    {
        const string url = "api/Payments/buy";

        using var content = JsonContent.Create(new BuyModel { TicketTypeId = ticketType, UserId = Guid.NewGuid() });
        using var response = await _httpClient.PostAsync(url, content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var success = bool.Parse(await response.Content.ReadAsStringAsync(cancellationToken));

        return success;
    }
}

