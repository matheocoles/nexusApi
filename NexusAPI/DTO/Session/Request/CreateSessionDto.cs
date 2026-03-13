using System.ComponentModel.DataAnnotations;

namespace NexusAPI.DTO.Session.Request;

public class CreateSessionDto
{
    
    [Required]
    public DateTime DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }
    public string? Status { get; set; }
    
    public int LoginId { get; set; } 

    public int? ClassId { get; set; }
    public int? SportId { get; set; }
    public int? ExtraActivityId { get; set; }
    public List<int>? AchievementIds { get; set; }
}