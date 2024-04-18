namespace Web.Dto
{
    public class EventDto
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateOnly Date { get; set; }
        
        public TimeOnly? StartTime { get; set; }
        
        public TimeOnly? EndTime { get; set; }
        
        public Guid OrganizationId { get; set; }
        
        public string OrganizationName { get; set; }
        
        public bool IsPast { get; set; }
        
        public string? ImageName { get; set; }
    }
}