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
        // Création de la session
        var session = new Models.Session()
        {
            DateTimeEnd = req.DateTimeEnd,
            DateTimeStart = req.DateTimeStart,
            Status = req.Status,
        };

        db.Sessions.Add(session);
        await db.SaveChangesAsync(ct);

        // Gestion des achievements associés si fournis
        if (req.AchievementIds != null && req.AchievementIds.Any())
        {
            foreach (var achievementId in req.AchievementIds)
            {
                db.SessionAchievements.Add(new SessionAchievement
                {
                    SessionId = session.Id,
                    AchievementId = achievementId
                });
            }

            await db.SaveChangesAsync(ct);
        }

        // Préparation de la réponse avec achievements
        var achievements = await db.SessionAchievements
            .Where(sa => sa.SessionId == session.Id)
            .Include(sa => sa.Achievement)
            .Select(sa => new GetAchievementDto
            {
                Id = sa.Achievement.Id,
                Name = sa.Achievement.Name,
                Description = sa.Achievement.Description
            })
            .ToListAsync(ct);

        var response = new GetSessionDto
        {
            Id = session.Id,
            DateTimeEnd = session.DateTimeEnd,
            DateTimeStart = session.DateTimeStart,
            Status = session.Status,
            Achievements = achievements
            
        };

        await Send.OkAsync(response, ct);
    }
}
