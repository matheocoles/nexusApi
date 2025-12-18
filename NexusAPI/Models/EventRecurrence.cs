namespace NexusAPI.Models;

public class EventRecurrence
{
    public Type Type { get; set; }
    public int Frequency { get; set; }
    public DateOnly? DateEnd { get; set; }
    public DayOfWeek? Day { get; set; }
}