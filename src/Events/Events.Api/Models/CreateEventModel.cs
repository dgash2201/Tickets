namespace Events.Api.Models
{
    public class CreateEventModel
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateOnly Date { get; set; }
        
        public Guid OrganizationId { get; set; }
        
        public string? ImageName { get; set; }
    }
}