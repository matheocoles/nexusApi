using NexusAPI.DTO.Achievement.Request;
using NexusAPI.DTO.Achievement.Response;
using NexusAPI.DTO.Session.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Achievement;

public class UpdateAchievementEndpoint(NexusDbContext db)
    : Endpoint<UpdateAchievementDto, GetAchievementDto>
{
    public override void Configure()
    {
        Put("/achievements/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAchievementDto req, CancellationToken ct)
    {
        // Charger l'achievement avec ses liaisons
        var achievementToEdit = await db.Achievements
            .Include(a => a.SessionAchievements)
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (achievementToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Mettre à jour les champs
        achievementToEdit.Name = req.Name;
        achievementToEdit.Description = req.Description;

        // Mettre à jour les liaisons avec les sessions si fournies
        if (req.SessionIds != null)
        {
            // Supprimer les anciennes liaisons
            db.SessionAchievements.RemoveRange(achievementToEdit.SessionAchievements);

            // Ajouter les nouvelles liaisons
            foreach (var sessionId in req.SessionIds)
            {
                db.SessionAchievements.Add(new SessionAchievement
                {
                    AchievementId = achievementToEdit.Id,
                    SessionId = sessionId
                });
            }
        }

        await db.SaveChangesAsync(ct);

        // Préparer la liste des sessions liées pour le DTO de réponse
        var sessions = await db.SessionAchievements
            .Where(sa => sa.AchievementId == achievementToEdit.Id)
            .Include(sa => sa.Session)
            .Select(sa => new GetSessionDto
            {
                Id = sa.Session.Id,
                DateTimeStart = sa.Session.DateTimeStart,
                DateTimeEnd = sa.Session.DateTimeEnd,
                Status = sa.Session.Status,
                Achievements = new List<GetAchievementDto>() // vide pour éviter récursion infinie
            })
            .ToListAsync(ct);

        // Construire le DTO de réponse
        var responseDto = new GetAchievementDto
        {
            Id = achievementToEdit.Id,
            Name = achievementToEdit.Name,
            Description = achievementToEdit.Description,
            Sessions = sessions
        };

        await Send.OkAsync(responseDto, ct);
    }
}
