using Events.Application.Ports;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace Events.Infrastructure.Clients
{
    public class OrganizationsGrpcGrpcClient : IOrganizationsGrpcClient
    {
        private readonly Organizations.OrganizationsClient _organizationsClient;
        
        public OrganizationsGrpcGrpcClient(IConfiguration configuration)
        {
            var channel = GrpcChannel.ForAddress(configuration["OrganizationsGrpc"]!);
            _organizationsClient = new Organizations.OrganizationsClient(channel);
        }

        public async Task<string> GetOrganizationName(Guid id)
        {
            var response = await _organizationsClient.GetOrganizationNameAsync(new GetOrganizationNameRequest
                    { Id = id.ToString() });
            return response.Name;
        }
    }
}