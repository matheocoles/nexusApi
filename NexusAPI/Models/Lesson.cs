namespace NexusAPI.Models;

public class Lesson : Activity
{
    public string? Subject { get; set; }
    public string? Teacher { get; set; }
    public string? Room { get; set; }
    public string? Objective { get; set; }
}