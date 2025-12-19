using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Models;

[PrimaryKey(nameof(ActivityId), nameof(SessionId))]
public class ActivitySession
{
    public Activity? Activity { get; set; }
    [Required] public int ActivityId { get; set; }
    
    public Session? Session { get; set; }
    [Required] public int? SessionId { get; set; }
}