using NexusAPI.DTO.Achievement.Response;
using NexusAPI.DTO.Session.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Achievement;

public class GetAllAchievementEndpoint(NexusDbContext db)
    : EndpointWithoutRequest<List<GetAchievementDto>>
{
    public override void Configure()
    {
        Get("/achievements");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var responseDto = await db.Achievements
            .Include(a => a.SessionAchievements)       // charger les liaisons
            .ThenInclude(sa => sa.Session)         // charger les sessions liées
            .Select(a => new GetAchievementDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Sessions = a.SessionAchievements
                    .Select(sa => new GetSessionDto
                    {
                        Id = sa.Session.Id,
                        DateTimeStart = sa.Session.DateTimeStart,
                        DateTimeEnd = sa.Session.DateTimeEnd,
                        Status = sa.Session.Status,
                        Achievements = new List<GetAchievementDto>() // vide pour éviter récursion infinie
                    })
                    .ToList()
            })
            .AsNoTracking()
            .ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}