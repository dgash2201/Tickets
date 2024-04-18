using Events.Api.Models;
using Events.Application.Dto;
using Events.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Events.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{eventId:guid}")]
        public async Task<EventDto> GetEvent(Guid eventId)
        {
            return await _eventService.GetById(eventId);
        }

        [HttpPost]
        public async Task<Guid> CreateEvent(CreateEventModel model)
        {
            return await _eventService.Create(
                model.Title,
                model.Description,
                model.Date,
                model.OrganizationId,
                model.ImageName);
        }

        [HttpGet("Search")]
        public async Task<IEnumerable<EventDto>> Search(string? search)
        {
            return await _eventService.SearchEvents(search);
        }
        
        [HttpGet]
        public async Task<IEnumerable<EventDto>> GetByOrganizationId(Guid organizationId)
        {
            return await _eventService.GetByOrganizationId(organizationId, false);
        }

        [HttpGet("Future")]
        public async Task<IEnumerable<EventDto>> GetFutureEvents(Guid organizationId)
        {
            return await _eventService.GetByOrganizationId(organizationId);
        }

        [HttpGet("GetByIds")]
        public async Task<IEnumerable<EventDto>> GetByIds(IEnumerable<Guid> ids)
        {
            return await _eventService.GetByIds(ids);
        }
    }
}