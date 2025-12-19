using NexusAPI.DTO.Achievement.Response;
using NexusAPI.DTO.Session.Response;

namespace NexusAPI.DTO.SessionAchievement.Response;

public class GetSessionAchievementDto
{
    public int SessionId { get; set; }
    public GetSessionDto Session { get; set; } = null!;

    public int AchievementId { get; set; }
    public GetAchievementDto Achievement { get; set; } = null!;
}