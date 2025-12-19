using NexusAPI.DTO.Achievement.Request;
using NexusAPI.DTO.Achievement.Response;
using NexusAPI.DTO.Session.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Achievement;

public class CreateAchievementEndpoint(NexusDbContext db)
    : Endpoint<CreateAchievementDto, GetAchievementDto>
{
    public override void Configure()
    {
        Post("/achievements");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAchievementDto req, CancellationToken ct)
    {
        // Création de l'achievement
        var achievement = new Models.Achievement()
        {
            Name = req.Name,
            Description = req.Description,
        };
        
        db.Achievements.Add(achievement);
        await db.SaveChangesAsync(ct);

        // Charger les sessions liées (aucune pour un nouvel achievement, mais on prépare la liste)
        var sessions = await db.SessionAchievements
            .Where(sa => sa.AchievementId == achievement.Id)
            .Include(sa => sa.Session)
            .Select(sa => new GetSessionDto
            {
                Id = sa.Session.Id,
                DateTimeStart = sa.Session.DateTimeStart,
                DateTimeEnd = sa.Session.DateTimeEnd,
                Status = sa.Session.Status,
                Achievements = new List<GetAchievementDto>() // vide pour éviter la récursion infinie
            })
            .ToListAsync(ct);

        // Préparer le DTO de réponse
        var response = new GetAchievementDto
        {
            Id = achievement.Id,
            Name = achievement.Name,
            Description = achievement.Description,
            Sessions = sessions
        };
        
        await Send.OkAsync(response, ct);
    }
}