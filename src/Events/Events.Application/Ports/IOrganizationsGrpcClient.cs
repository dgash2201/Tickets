namespace Events.Application.Ports
{
    public interface IOrganizationsGrpcClient
    {
        Task<string> GetOrganizationName(Guid id);
    }
}