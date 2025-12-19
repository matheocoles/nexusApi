using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models;

public class Achievement
{
    [Key] public int Id { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }
    
    public List<SessionAchievement> SessionAchievements { get; set; } = new();

}