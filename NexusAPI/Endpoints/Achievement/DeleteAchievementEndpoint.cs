using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Achievement;

public class DeleteAchievementRequest
{
    public int Id { get; set; }
}

public class DeleteAchievementEndpoint(NexusDbContext db) : Endpoint<DeleteAchievementRequest>
{
    public override void Configure()
    {
        Delete("/achievements/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteAchievementRequest req, CancellationToken ct)
    {
        // Récupérer l'achievement avec ses liaisons
        var achievementToDelete = await db.Achievements
            .Include(a => a.SessionAchievements) // Charger les liaisons
            .SingleOrDefaultAsync(p => p.Id == req.Id, ct);

        if (achievementToDelete == null)
        {
            Console.WriteLine($"Aucun achievement avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        // Supprimer toutes les liaisons avec les sessions
        if (achievementToDelete.SessionAchievements.Any())
        {
            db.SessionAchievements.RemoveRange(achievementToDelete.SessionAchievements);
        }

        // Supprimer l'achievement
        db.Achievements.Remove(achievementToDelete);
        await db.SaveChangesAsync(ct);

        Console.WriteLine($"Achievement {req.Id} a été supprimé avec succès.");
        await Send.NoContentAsync(ct);
    }
}