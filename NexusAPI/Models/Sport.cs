namespace NexusAPI.Models;

public class Sport : Activity
{
    public string? Type { get; set; }
    public string? Place { get; set; }
    public int Duration { get; set; }
    public string? Intensity { get; set; }
}