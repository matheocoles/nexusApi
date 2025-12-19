using NexusAPI.DTO.Achievement.Response;

namespace NexusAPI.DTO.Session.Request;

public class CreateSessionDto
{
    public int Id { get; set; }
    public DateOnly? DateTimeStart { get; set; }
    public DateOnly? DateTimeEnd { get; set; }
    public string? Status { get; set; }
    
    public List<GetAchievementDto>? Achievements { get; set; }
    public List<int>? AchievementIds { get; set; }


}