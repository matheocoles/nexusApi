namespace NexusAPI.DTO.EventRecurrence.Response;

public class GetEventRecurrenceDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Frequency { get; set; }
    public DateOnly? DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }
    public DayOfWeek? Day { get; set; }
}