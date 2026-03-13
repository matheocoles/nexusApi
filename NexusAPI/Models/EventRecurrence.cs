using System.ComponentModel.DataAnnotations;
using NexusAPI.Models.Enums;

namespace NexusAPI.Models;

public class EventRecurrence
{
    [Key] public int Id { get; set; }
    [Required] public RecurrenceType Type { get; set; }
    [Required] public int Frequency { get; set; }
    [Required] public DateOnly? DateStart { get; set; }
    [Required] public DateOnly? DateEnd { get; set; }
    public DayOfWeek? Day { get; set; }
    
    public List<Session>? Sessions { get; set; }
}