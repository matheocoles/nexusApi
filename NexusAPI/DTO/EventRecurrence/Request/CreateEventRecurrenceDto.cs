using NexusAPI.Models.Enums;


namespace NexusAPI.DTO.EventRecurrence.Request;

public class CreateEventRecurrenceDto
{
    public RecurrenceType Type { get; set; }
    public int Frequency { get; set; }
    public DateOnly? DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
    public DayOfWeek? Day { get; set; }
}