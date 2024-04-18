namespace Organizations.Api.Models.Requests;

public class RegisterUserRequestModel
{
    public string Login { get; set; }
    
    public string Name { get; set; }
    
    public string Password { get; set; }
}