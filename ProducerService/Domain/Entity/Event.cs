namespace ProducerService.Domain.Entity;

public class Event
{
    public int Id { get; set; }
    public string ProducerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}