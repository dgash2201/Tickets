using Common.Application.Exceptions;

namespace Events.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; private set; }
        
        public string Title { get; private set; }
        
        public string Description { get; private set; }
        
        public DateOnly Date { get; private set; }
        
        public Guid OrganizationId { get; private set; }
        
        public bool IsDeleted { get; private set; }
        
        public string? ImageName { get; private set; }

        public bool IsPast => DateOnly.FromDateTime(DateTime.UtcNow) > Date;
        
        public Event(
            string title,
            string description,
            DateOnly date,
            Guid organizationId,
            string? imageName)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Date = date;
            OrganizationId = organizationId;
            IsDeleted = false;
            ImageName = imageName;
        }

        public void Update(
            string title,
            string description,
            DateOnly date)
        {
            if (IsPast || IsDeleted)
            {
                throw new DomainException("Информация о мероприятии не может быть обновлена");
            }
            
            Title = title;
            Description = description;
            Date = date;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
        
        private Event() {}
    }
}