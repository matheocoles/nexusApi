using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Endpoints.Achievement;
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
        Delete("achievements{id}", x => new { x.Id });
    }

    public override async Task HandleAsync(DeleteAchievementRequest req, CancellationToken ct)
    {
        var sessionToDelete = await db.Sessions
            .SingleOrDefaultAsync(p => p.Id == req.Id, ct);
    
        if (sessionToDelete is null)
        {
            Console.WriteLine($"Aucun session avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }
    
        db.Sessions.Remove(sessionToDelete);

        await db.SaveChangesAsync(ct);
        Console.WriteLine($"Session {req.Id}a été supprimé avec succès.");

        await Send.NoContentAsync(ct);
    }
}