namespace NexusAPI.DTO.SmartReminder.Request;

public class CreateSmartReminderDto
{
    public DateOnly? DateAlert { get; set; }
    public DateOnly? TimeAlert { get; set; }
    public Type? Type { get; set; }
    public string? Status { get; set; }
}