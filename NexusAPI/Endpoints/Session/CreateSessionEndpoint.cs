using NexusAPI.DTO.Session.Request;
using NexusAPI.DTO.Session.Response;
using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Session;

public class CreateSessionEndpoint(NexusDbContext db)
    : Endpoint<CreateSessionDto, GetSessionDto>
{
    public override void Configure()
    {
        Post("/sessions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSessionDto req, CancellationToken ct)
{
    var session = new Models.Session()
    {
        DateTimeStart = req.DateTimeStart,
        DateTimeEnd = req.DateTimeEnd,
        Status = req.Status,
        LoginId = req.LoginId,
        ClassId = req.ClassId,
        SportId = req.SportId,
        ExtraActivityId = req.ExtraActivityId
    };

    db.Sessions.Add(session);
    await db.SaveChangesAsync(ct);

    if (req.AchievementIds != null && req.AchievementIds.Any())
    {
        var sessionAchievements = req.AchievementIds.Select(id => new SessionAchievement 
        { 
            SessionId = session.Id, 
            AchievementId = id 
        });

        await db.SessionAchievements.AddRangeAsync(sessionAchievements, ct);
        await db.SaveChangesAsync(ct);
    }

    var response = new GetSessionDto
    {
        Id = session.Id,
        DateTimeStart = session.DateTimeStart,
        DateTimeEnd = session.DateTimeEnd,
        Status = session.Status,
        ActivityName = await GetActivityName(session, ct) 
    };

    await Send.OkAsync(response, ct);
}

private async Task<string> GetActivityName(Models.Session s, CancellationToken ct)
{
    if (s.ClassId.HasValue) 
        return (await db.Classes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == s.ClassId, ct))?.Name ?? "Cours inconnu";
            
    if (s.SportId.HasValue) 
        return (await db.Sports.AsNoTracking().FirstOrDefaultAsync(sp => sp.Id == s.SportId, ct))?.Name ?? "Sport inconnu";
            
    if (s.ExtraActivityId.HasValue) 
        return (await db.ExtraActivities.AsNoTracking().FirstOrDefaultAsync(e => e.Id == s.ExtraActivityId, ct))?.Name ?? "Activité inconnue";

    return s.Status ?? "Session Libre";
}

}