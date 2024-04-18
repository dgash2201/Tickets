using Events.Application.Dto;

namespace Events.Application.Interfaces
{
    public interface IEventService
    {
        Task<EventDto> GetById(Guid eventId);

        Task<IEnumerable<EventDto>> GetByOrganizationId(Guid organizationId, bool onlyFuture = true);
        
        Task<IEnumerable<EventDto>> SearchEvents(string? searchString);

        Task<Guid> Create(string title, string description, DateOnly date, Guid organizationId, string? imageName);

        Task Update(Guid eventId, string title, string description, DateOnly date);

        Task Delete(Guid eventId);

        Task<IEnumerable<EventDto>> GetByIds(IEnumerable<Guid> ids);
    }
}