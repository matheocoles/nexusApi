using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Sport.Response;

namespace NexusAPI.Endpoints.Sport;

public class GetSportEndpoint(NexusDbContext nexusDbContext) :Endpoint<GetSportDto>
{
    public override void Configure()
    {
        Get("/sport/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSportDto req, CancellationToken ct)
    {
        
        Models.Sport? sport = await nexusDbContext
            .Sports
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (sport == null)
        {
            Console.WriteLine($"Aucun sport avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetSportDto responseDto = new()
        {
            Id = req.Id, 
            Name = sport.Name,
            Description = sport.Description,
            Place = sport.Place,
            Duration = sport.Duration,
            Intensity = sport.Intensity,
        };

        await Send.OkAsync(responseDto, ct);
    }
}