using Common.Application.Exceptions;
using Events.Application.Dto;
using Events.Application.Interfaces;
using Events.Application.Ports;
using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Events.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IApplicationContext _context;
        private readonly IOrganizationsGrpcClient _organizationsGrpcClient;

        public EventService(
            IApplicationContext context, 
            IOrganizationsGrpcClient organizationsGrpcClient)
        {
            _context = context;
            _organizationsGrpcClient = organizationsGrpcClient;
        }

        public async Task<EventDto> GetById(Guid eventId)
        {
            var _event = await GetEventById(eventId);
            var organizationName = await _organizationsGrpcClient.GetOrganizationName(_event.OrganizationId);

            return new EventDto(_event, organizationName);
        }

        public async Task<IEnumerable<EventDto>> GetByOrganizationId(Guid organizationId, bool onlyFuture = true)
        {
            var events = await _context.Events
                .Where(_event => _event.OrganizationId == organizationId && !_event.IsDeleted &&
                                 (!onlyFuture || DateOnly.FromDateTime(DateTime.UtcNow) < _event.Date))
                .ToListAsync();

            var result = new List<EventDto>();

            foreach (var _event in events)
            {
                var organizationName = await _organizationsGrpcClient.GetOrganizationName(_event.OrganizationId);

                result.Add(new EventDto(_event, organizationName));
            }

            return result;
        }
        
        public async Task<IEnumerable<EventDto>> SearchEvents(string? searchString)
        {
            var events = await _context.Events.Where(_event =>
                !_event.IsDeleted &&
                (searchString == null || _event.Title.ToLower().Contains(searchString.ToLower())))
                .ToListAsync();

            var result = new List<EventDto>();

            foreach (var _event in events)
            {
                var organizationName = await _organizationsGrpcClient.GetOrganizationName(_event.OrganizationId);

                result.Add(new EventDto(_event, organizationName));
            }

            return result;
        }

        public async Task<Guid> Create(
            string title,
            string description,
            DateOnly date,
            Guid organizationId,
            string? imageName)
        {
            var _event = new Event(title, description, date, organizationId, imageName);

            _context.Events.Add(_event);
            await _context.SaveChangesAsync();

            return _event.Id;
        }

        public async Task Update(
            Guid eventId,
            string title,
            string description,
            DateOnly date)
        {
            var _event = await GetEventById(eventId);
            
            _event.Update(title, description, date);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid eventId)
        {
            var _event = await GetEventById(eventId);
            
            _event.Delete();

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventDto>> GetByIds(IEnumerable<Guid> ids)
        {
            var events = await _context.Events
                .Where(_event => ids.Contains(_event.Id))
                .ToListAsync();

            var result = new List<EventDto>();

            foreach (var _event in events)
            {
                var organizationName = await _organizationsGrpcClient.GetOrganizationName(_event.OrganizationId);

                result.Add(new EventDto(_event, organizationName));
            }

            return result;
        }

        private async Task<Event> GetEventById(Guid eventId)
        {
            return await _context.Events
                       .Where(_event => !_event.IsDeleted)
                       .FirstOrDefaultAsync(_event => _event.Id == eventId)
                   ?? throw new EntityNotFoundException("Мероприятие не найдено");
        }
    }
}