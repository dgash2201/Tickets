using Web.Dto;

namespace Web.Interfaces
{
    public interface IEventService
    {
        Task Create(CreateEventDto dto);
    }
}