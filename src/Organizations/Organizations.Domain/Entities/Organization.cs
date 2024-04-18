namespace Organizations.Domain.Entities;

public class Organization : User
{
    public string? Description { get; private set; }
    
    public string Inn { get; private set; }

    public string Ogrn { get; private set; }

    public Organization(string login, string name, string passwordHash, string inn, string ogrn)
        : base(login, name, passwordHash, UserType.Organization)
    {
        Inn = inn;
        Ogrn = ogrn;
    }
        
    private Organization() { }
}