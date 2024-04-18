namespace Organizations.Application.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    
    public string Login { get; set; }
    
    public string Name { get; set; }
    
    public string PasswordHash { get; set; }
    
    public UserType UserType { get; set; }
}