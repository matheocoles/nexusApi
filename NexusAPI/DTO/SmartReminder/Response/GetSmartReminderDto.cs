namespace NexusAPI.DTO.SmartReminder.Response;

public class GetSmartReminderDto
{
    public int Id { get; set; }
    public DateOnly? DateAlert { get; set; }
    public DateOnly? TimeAlert { get; set; }
    public Type? Type { get; set; }
    public string? Status { get; set; }
}