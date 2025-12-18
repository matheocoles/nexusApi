namespace NexusAPI.DTO.Session.Request;

public class CreateSessionDto
{
    public DateOnly? DateTimeStart { get; set; }
    public DateOnly? DateTimeEnd { get; set; }
    public string? Status { get; set; }

}