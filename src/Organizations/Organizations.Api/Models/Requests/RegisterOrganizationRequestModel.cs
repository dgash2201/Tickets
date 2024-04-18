namespace Organizations.Api.Models.Requests;

public class RegisterOrganizationRequestModel : RegisterUserRequestModel
{
    public string Inn { get; set; }
    
    public string Ogrn { get; set; }
}