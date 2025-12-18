using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Activity;

public class DeleteActivityRequest
{
    public int Id { get; set; }
}

public class DeleteActivityEndpoint(NexusDbContext nexusDbContext) : Endpoint<DeleteActivityRequest>
{
    public override void Configure()
    {
        Delete("/activity/{@id}", x => new { x.Id });
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(DeleteActivityRequest req, CancellationToken ct)
    {
        
        Models.Activity? activityToDelete = await nexusDbContext
            .Activities
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (activityToDelete == null)
        {
            Console.WriteLine($"Aucune activité avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        nexusDbContext.Activities.Remove(activityToDelete);
        await nexusDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}