using Events.Domain.Entities;

namespace Events.Application.Dto
{
    public class EventDto
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateOnly Date { get; set; }
        
        public Guid OrganizationId { get; set; }
        
        public string OrganizationName { get; set; }
        
        public bool IsPast { get; set; }
        
        public string? ImageName { get; set; }

        public EventDto(Event _event, string organizationName)
        {
            Id = _event.Id;
            Title = _event.Title;
            Description = _event.Description;
            Date = _event.Date;
            OrganizationId = _event.OrganizationId;
            IsPast = _event.IsPast;
            ImageName = _event.ImageName;
            OrganizationName = organizationName;
        }
    }
}