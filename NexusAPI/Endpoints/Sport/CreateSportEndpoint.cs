using FastEndpoints;
using NexusAPI.DTO.Sport.Request;
using NexusAPI.DTO.Sport.Response;

namespace NexusAPI.Endpoints.Sport;

public class CreateSportEndpoint(NexusDbContext nexusDbContext) : Endpoint<CreateSportDto, GetSportDto>
{
    public override void Configure()
    {
        Post("/sport");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSportDto req, CancellationToken ct)
    {
        Models.Sport sport = new()
        {
            Name = req.Name,
            Description = req.Description,
            Type = req.Type,
            Place = req.Place,
            Duration = req.Duration,
            Intensity = req.Intensity,
        };
        
        nexusDbContext.Sports.Add(sport);
        await nexusDbContext.SaveChangesAsync(ct);
        
        Console.WriteLine($"Created sport {sport.Name}");

        GetSportDto sportDto = new()
        {
            Name = sport.Name,
            Description = sport.Description,
            Type = sport.Type,
            Place = sport.Place,
            Duration = sport.Duration,
            Intensity = sport.Intensity,
        };
        
        await nexusDbContext.AddAsync(sportDto, ct);
    }
}