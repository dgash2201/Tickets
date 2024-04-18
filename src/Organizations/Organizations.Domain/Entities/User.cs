namespace Organizations.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    
    public string Login { get; private set; }
    
    public string Name { get; private set; }
    
    public string PasswordHash { get; private set; }
    
    public UserType UserType { get; private set; }
    
    public User(string login, string name, string passwordHash, UserType userType = UserType.User)
    {
        Login = login;
        Name = name;
        PasswordHash = passwordHash;
        UserType = userType;
    }
    
    protected User() { }
}