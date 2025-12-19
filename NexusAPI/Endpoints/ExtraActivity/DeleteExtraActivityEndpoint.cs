using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.ExtraActivity;

public class DeleteExtraActivityRequest
{
    public int Id { get; set; }
}

public class DeleteExtraActivityEndpoint(NexusDbContext nexusDbContext) : Endpoint<DeleteExtraActivityRequest>
{
    public override void Configure()
    {
        Delete("/extraactivity/{@id}", x => new { x.Id });
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(DeleteExtraActivityRequest req, CancellationToken ct)
    {
        
        Models.ExtraActivity? extraActivityToDelete = await nexusDbContext
            .ExtraActivities
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (extraActivityToDelete == null)
        {
            Console.WriteLine($"Aucune extraActivité avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        nexusDbContext.ExtraActivities.Remove(extraActivityToDelete);
        await nexusDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}