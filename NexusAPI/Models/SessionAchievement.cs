namespace NexusAPI.Models;

public class SessionAchievement
{
    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;

    public int AchievementId { get; set; }
    public Achievement Achievement { get; set; } = null!;
}
