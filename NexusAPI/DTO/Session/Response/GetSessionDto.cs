namespace NexusAPI.DTO.Session.Response;

public class GetSessionDto
{
    public int Id { get; set; }
    public DateOnly? DateTimeStart { get; set; }
    public DateOnly? DateTimeEnd { get; set; }
    public string? Status { get; set; }
    
}