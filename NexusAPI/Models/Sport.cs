using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models;

public class Sport : Activity
{
    [Required] public string? Type { get; set; }
    [Required] public string? Place { get; set; }
    [Required] public int Duration { get; set; }
    [Required] public string? Intensity { get; set; }
}