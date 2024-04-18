namespace Web.Clients;

public class AuthClient
{
    private readonly HttpClient _httpClient;

    public AuthClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(configuration["AuthBaseAddress"]!);
    }
    
    public async Task<HttpResponseMessage> LoginAsync(string login, string password)
    {
        var requestModel = new { login, password };

        return await _httpClient.PostAsJsonAsync("login", requestModel);
    }
}