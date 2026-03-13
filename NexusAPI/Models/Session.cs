namespace NexusAPI.Models;

public class Session
{
    public int Id { get; set; }
    public DateTime? DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }
    public string? Status { get; set; }
    
    public int LoginId { get; set; }

    public int? ClassId { get; set; }
    public Class? Class { get; set; }

    public int? SportId { get; set; }
    public Sport? Sport { get; set; }

    public int? ExtraActivityId { get; set; }
    public ExtraActivity? ExtraActivity { get; set; }

    public ICollection<SessionAchievement> SessionAchievements { get; set; } = new List<SessionAchievement>();
}