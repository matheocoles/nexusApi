using NexusAPI.DTO.Session.Request;
using NexusAPI.DTO.Session.Response;
using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Session;

public class UpdateSessionEndpoint(NexusDbContext db)
    : Endpoint<UpdateSessionDto, GetSessionDto>
{
    public override void Configure()
    {
        Put("/sessions/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateSessionDto req, CancellationToken ct)
    {
        // Charger la session avec ses achievements
        var sessionToEdit = await db.Sessions
            .Include(s => s.SessionAchievements)
            .ThenInclude(sa => sa.Achievement)
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (sessionToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Mettre à jour les champs
        sessionToEdit.DateTimeStart = req.DateTimeStart;
        sessionToEdit.DateTimeEnd = req.DateTimeEnd;
        sessionToEdit.Status = req.Status;

        await db.SaveChangesAsync(ct);

        // Préparer la liste des achievements pour le DTO
        var achievements = sessionToEdit.SessionAchievements
            .Select(sa => new GetAchievementDto
            {
                Id = sa.Achievement.Id,
                Name = sa.Achievement.Name,
                Description = sa.Achievement.Description
            })
            .ToList();

        // Construire le DTO de réponse
        var responseDto = new GetSessionDto
        {
            Id = sessionToEdit.Id,
            DateTimeStart = sessionToEdit.DateTimeStart,
            DateTimeEnd = sessionToEdit.DateTimeEnd,
            Status = sessionToEdit.Status,
            Achievements = achievements
        };

        await Send.OkAsync(responseDto, ct);
    }
}