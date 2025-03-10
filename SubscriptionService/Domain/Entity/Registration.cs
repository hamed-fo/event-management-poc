namespace SubscriptionService.Domain.Entity;

public class Registration
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}