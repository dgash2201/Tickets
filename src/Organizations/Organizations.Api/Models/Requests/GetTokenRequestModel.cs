namespace Organizations.Api.Models.Requests;

public class GetTokenRequestModel
{
    public string Login { get; set; }
    
    public string Password { get; set; }
}