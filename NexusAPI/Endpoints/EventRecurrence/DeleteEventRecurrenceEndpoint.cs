using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.EventRecurrence;

public class DeleteEventRecurrenceResponse
{
    public int Id { get; set; }
}

public class DeleteActivityEndpoint(NexusDbContext nexusDbContext) : Endpoint<DeleteEventRecurrenceResponse>
{
    public override void Configure()
    {
        Delete("/eventrecurrence/{@id}", x => new { x.Id });
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(DeleteEventRecurrenceResponse req, CancellationToken ct)
    {
        
        Models.EventRecurrence? eventrecurrenceToDelete = await nexusDbContext
            .EventRecurrence
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (eventrecurrenceToDelete == null)
        {
            Console.WriteLine($"Aucun événement avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        nexusDbContext.EventRecurrence.Remove(eventrecurrenceToDelete);
        await nexusDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}