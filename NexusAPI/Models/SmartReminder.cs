using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models;

public class SmartReminder
{
    [Key] public int Id { get; set; }
    [Required] public DateOnly? DateAlert { get; set; }
    [Required] public DateOnly? TimeAlert { get; set; }
    [Required] public Type? Type { get; set; }
    [Required] public string? Status { get; set; }
    
    [Required] public int SessionId { get; set; }
    [Required] public Session? Session { get; set; }
}