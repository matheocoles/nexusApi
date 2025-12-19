using NexusAPI.DTO.Achievement.Response;
using NexusAPI.DTO.Session.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Achievement;

public class GetAchievementRequest
{
    public int Id { get; set; }
}

public class GetAchievementEndpoint(NexusDbContext db)
    : Endpoint<GetAchievementRequest, GetAchievementDto>
{
    public override void Configure()
    {
        Get("/achievements/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAchievementRequest req, CancellationToken ct)
    {
        // Charger l'achievement avec les sessions liées
        var achievement = await db.Achievements
            .Include(a => a.SessionAchievements)
            .ThenInclude(sa => sa.Session)
            .SingleOrDefaultAsync(a => a.Id == req.Id, ct);

        if (achievement == null)
        {
            Console.WriteLine($"Aucun trophée avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        // Transformer les sessions liées en DTO
        var sessions = achievement.SessionAchievements
            .Select(sa => new GetSessionDto
            {
                Id = sa.Session.Id,
                DateTimeStart = sa.Session.DateTimeStart,
                DateTimeEnd = sa.Session.DateTimeEnd,
                Status = sa.Session.Status,
                Achievements = new List<GetAchievementDto>() // vide pour éviter récursion infinie
            })
            .ToList();

        // Préparer le DTO de réponse
        var responseDto = new GetAchievementDto
        {
            Id = achievement.Id,
            Name = achievement.Name,
            Description = achievement.Description,
            Sessions = sessions
        };

        await Send.OkAsync(responseDto, ct);
    }
}