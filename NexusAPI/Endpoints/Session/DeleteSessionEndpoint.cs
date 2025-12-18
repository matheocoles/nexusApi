using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Session;

public class DeleteSessionRequest
{
    public int Id { get; set; }
}

public class DeleteSessionEndpoint(NexusDbContext db) : Endpoint<DeleteSessionRequest>
{
    public override void Configure()
    {
        Delete("/sessions/{id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteSessionRequest req, CancellationToken ct)
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
