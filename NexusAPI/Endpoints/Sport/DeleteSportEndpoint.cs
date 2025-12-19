using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Sport;

public class DeleteSportRequest
{
    public int Id { get; set; }
}

public class DeleteSportEndpoint(NexusDbContext nexusDbContext) : Endpoint<DeleteSportRequest>
{
    public override void Configure()
    {
        Delete("/sport/{@id}", x => new { x.Id });
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(DeleteSportRequest req, CancellationToken ct)
    {
        
        Models.Sport? sportToDelete = await nexusDbContext
            .Sports
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (sportToDelete == null)
        {
            Console.WriteLine($"Aucun sport avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        nexusDbContext.Sports.Remove(sportToDelete);
        await nexusDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}