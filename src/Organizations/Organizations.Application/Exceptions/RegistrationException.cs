namespace Organizations.Application.Exceptions;

public class RegistrationException : Exception
{
    public RegistrationException(string message) : base(message) { }
}