using Grpc.Core;
using Organizations.Application.Services;

namespace Organizations.Grpc.Services
{
    public class OrganizationsService : Organizations.OrganizationsBase
    {
        private readonly OrganizationService _organizationService;

        public OrganizationsService(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public override async Task<GetOrganizationNameResponse> GetOrganizationName(GetOrganizationNameRequest request, ServerCallContext context)
        {
            var organizationId = Guid.Parse(request.Id);
            var organization = await _organizationService.GetById(organizationId);
            return new GetOrganizationNameResponse { Name = organization?.Name };
        }
    }
}