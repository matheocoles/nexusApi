using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models;

public class Lesson : Activity
{
    [Required] public string? Subject { get; set; }
    [Required] public string? Teacher { get; set; }
    [Required] public string? Room { get; set; }
    [Required] public string? Objective { get; set; }
}