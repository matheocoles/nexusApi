using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models;

public class EventRecurrence
{
    [Key] public int Id { get; set; }
    [Required] public Type Type { get; set; }
    [Required] public int Frequency { get; set; }
    [Required] public DateOnly? DateEnd { get; set; }
    [Required] public DayOfWeek? Day { get; set; }
}