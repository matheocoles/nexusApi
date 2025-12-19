using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Activity.Response;
using NexusAPI.DTO.ExtraActivity.Response;

namespace NexusAPI.Endpoints.ExtraActivity;

public class GetExtraActivityEndpoint(NexusDbContext nexusDbContext) :Endpoint<GetExtraActivityDto>
{
    public override void Configure()
    {
        Get("/extraactivity/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetExtraActivityDto req, CancellationToken ct)
    {
        
        Models.ExtraActivity? extraActivity = await nexusDbContext
            .ExtraActivities
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (extraActivity == null)
        {
            Console.WriteLine($"Aucune extraActivité avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetExtraActivityDto responseDto = new()
        {
            Id = req.Id, 
            Name = extraActivity.Name,
            Description = extraActivity.Description,
            Organiser = extraActivity.Organiser,
            Place = extraActivity.Place,
            Theme = extraActivity.Theme,
            Resource = extraActivity.Resource,
        };

        await Send.OkAsync(responseDto, ct);
    }
}