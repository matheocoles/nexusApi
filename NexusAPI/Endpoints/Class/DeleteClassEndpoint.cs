using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Class;

public class DeleteClassRequest
{
    public int Id { get; set; }
}

public class DeleteClassEndpoint(NexusDbContext nexusDbContext) : Endpoint<DeleteClassRequest>
{
    public override void Configure()
    {
        Delete("/class/{@id}", x => new { x.Id });
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(DeleteClassRequest req, CancellationToken ct)
    {
        
        Models.Class? @classToDelete = await nexusDbContext
            .Classes
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (@classToDelete == null)
        {
            Console.WriteLine($"Aucun cours avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        nexusDbContext.Classes.Remove(classToDelete);
        await nexusDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}