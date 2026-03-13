using NexusAPI.DTO.Achievement.Response;

namespace NexusAPI.DTO.Session.Request;

public class UpdateSessionDto
{
    public int Id { get; set; }
    public DateTime? DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }
    public string? Status { get; set; }
    
    public List<GetAchievementDto>? Achievements { get; set; }

}