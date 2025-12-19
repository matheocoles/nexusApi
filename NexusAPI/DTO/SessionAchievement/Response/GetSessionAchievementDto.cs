namespace NexusAPI.DTO.SessionAchievement.Response;

public class GetSessionAchievementDto
{
    public int SessionId { get; set; }
    public Models.Session Session { get; set; } = null!;

    public int AchievementId { get; set; }
    public Models.Achievement Achievement { get; set; } = null!;

}