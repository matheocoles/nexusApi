using NexusAPI.DTO.Session.Response;

namespace NexusAPI.DTO.Achievement.Request;

public class UpdateAchievementDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public List<int>? SessionIds { get; set; }

}