using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models;

public class Session
{
    [Key] public int Id { get; set; }
    [Required] public DateOnly? DateTimeStart { get; set; }
    [Required] public DateOnly? DateTimeEnd { get; set; }
    [Required] public string? Status { get; set; }
}