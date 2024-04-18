using Web.Clients;
using Web.Dto;
using Web.Interfaces;

namespace Web.Services
{
    public class EventService : IEventService
    {
        private readonly IImageService _imageService;
        private readonly EventsClient _eventClient;

        public EventService(IImageService imageService, EventsClient eventClient)
        {
            _imageService = imageService;
            _eventClient = eventClient;
        }

        public async Task Create(CreateEventDto dto)
        {
            await _imageService.Save(dto.File);
            await _eventClient.Create(dto);
        }
    }
}