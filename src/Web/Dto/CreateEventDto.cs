namespace Web.Dto
{
    public class CreateEventDto
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateOnly Date { get; set; }
        
        public Guid OrganizationId { get; set; }
        
        public IFormFile File { get; set; }
    }
}