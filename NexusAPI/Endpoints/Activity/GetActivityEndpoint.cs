using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI;
using NexusAPI.DTO.Activity.Response;
using NexusAPI.Models;

public class GetActivityRequest
{
    public int Id { get; set; }
}

public class GetActivityEndpoint(NexusDbContext nexusDbContext) :Endpoint<GetActivityRequest, GetActivityDto>
{
    public override void Configure()
    {
        Get("/activity/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetActivityRequest req, CancellationToken ct)
    {
        
        Activity? activity = await nexusDbContext
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
            Name = activity.Name
        };

        await Send.OkAsync(responseDto, ct);
    }
}