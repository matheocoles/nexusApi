using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI;

public class NexusDbContext : DbContext
{
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<EventRecurrence> EventRecurrence { get; set; }
    public DbSet<ExtraActivity> ExtraActivities { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SmartReminder> SmartReminders { get; set; }
    public DbSet<Sport> Sports { get; set; }
}