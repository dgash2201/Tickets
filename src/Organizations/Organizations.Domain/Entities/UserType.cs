using System.ComponentModel;

namespace Organizations.Domain.Entities;

public enum UserType
{
    [Description(nameof(User))] User,
    [Description(nameof(Organization))] Organization
}