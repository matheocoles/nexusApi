namespace NexusAPI.Models;

public class SmartReminder
{
    public DateOnly? DateAlert { get; set; }
    public DateOnly? TimeAlert { get; set; }
    public Type? Type { get; set; }
    public string? Status { get; set; }
}