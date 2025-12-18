using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models;

public class ExtraActivity : Activity
{
    [Required] public string? Organiser { get; set; }
    [Required] public string? Place { get; set; }
    [Required] public string? Theme { get; set; }
    [Required] public string? Resource { get; set; }
}