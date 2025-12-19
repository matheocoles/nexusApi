namespace NexusAPI.DTO.Achievement.Request;

public class CreateAchievementDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    // Liste d'IDs de sessions à lier à l'achievement
    public List<int>? SessionIds { get; set; }
}