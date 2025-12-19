using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Activity.Response;

namespace NexusAPI.Endpoints.Activity;

public class GetActivityEndpoint(NexusDbContext nexusDbContext) :Endpoint<GetActivityDto>
{
    public override void Configure()
    {
        Get("/activity/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetActivityDto req, CancellationToken ct)
    {
        
        Models.Activity? activity = await nexusDbContext
            .Activities
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (activity == null)
        {
            Console.WriteLine($"Aucune activité avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetActivityDto responseDto = new()
        {
            Id = req.Id, 
            Name = activity.Name,
            Description = activity.Description,
        };

        await Send.OkAsync(responseDto, ct);
    }
}