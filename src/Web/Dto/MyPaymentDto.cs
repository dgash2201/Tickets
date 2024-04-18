namespace Web.Dto;

public class MyPaymentDto
{
    public PaymentDto Payment { get; set; }
    
    public TicketTypeDto TicketType { get; set; }
    
    public EventDto Event { get; set; }
    
    public OrganizationDto Organization { get; set; }
}