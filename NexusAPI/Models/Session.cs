using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models;

public class Session
{
    [Key] public int Id { get; set; }
    [Required] public DateOnly? DateTimeStart { get; set; }
    [Required] public DateOnly? DateTimeEnd { get; set; }
    [Required] public string? Status { get; set; }
    
    [Required] public int ActivityId { get; set; }
    [Required] public Activity? Activity { get; set; }
    [Required] public int EventRecurrenceId { get; set; }
    [Required] public EventRecurrence? EventRecurrence { get; set; }
    
    public List<SessionAchievement> SessionAchievements { get; set; } = new();
    
}