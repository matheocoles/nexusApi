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
    public DbSet<SessionAchievement> SessionAchievements { get; set; }
    public DbSet<Login>  Logins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<System.Type>();
        modelBuilder.Ignore<System.Reflection.CustomAttributeData>();
        
        modelBuilder.Entity<SessionAchievement>()
            .HasKey(sa => new { sa.SessionId, sa.AchievementId });
        

        modelBuilder.Entity<SessionAchievement>()
            .HasOne(sa => sa.Session)
            .WithMany(s => s.SessionAchievements)
            .HasForeignKey(sa => sa.SessionId);

        modelBuilder.Entity<SessionAchievement>()
            .HasOne(sa => sa.Achievement)
            .WithMany(a => a.SessionAchievements)
            .HasForeignKey(sa => sa.AchievementId);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString =
            "Server=romaric-thibault.fr;" + 
            "Database=nexus;" + 
            "User Id=nexus;" +
            "Password=nexus;" +
            "TrustServerCertificate=true;";

        optionsBuilder.UseSqlServer(connectionString);
    }

}