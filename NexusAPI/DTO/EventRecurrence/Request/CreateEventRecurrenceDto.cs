namespace NexusAPI.DTO.EventRecurrence.Request;

public class CreateEventRecurrenceDto
{
    public Type Type { get; set; }
    public int Frequency { get; set; }
    public DateOnly? DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
    public DayOfWeek? Day { get; set; }
}