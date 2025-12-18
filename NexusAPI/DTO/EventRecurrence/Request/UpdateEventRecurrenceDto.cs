namespace NexusAPI.DTO.EventRecurrence.Request;

public class UpdateEventRecurrenceDto
{
    public int Id { get; set; }
    public Type Type { get; set; }
    public int Frequency { get; set; }
    public DateOnly? DateEnd { get; set; }
    public DayOfWeek? Day { get; set; }
}