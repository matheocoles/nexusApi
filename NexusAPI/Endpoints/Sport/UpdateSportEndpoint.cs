using FastEndpoints;
using NexusAPI.DTO.Sport.Request;
using NexusAPI.DTO.Sport.Response;

namespace NexusAPI.Endpoints.Sport;

public class UpdateSportEndpoint(NexusDbContext nexusDbContext) : Endpoint<UpdateSportDto, GetSportDto>
{
    public override void Configure()
    {
        Put("/sport");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateSportDto req, CancellationToken ct)
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
        
        nexusDbContext.Sports.Update(sport);
        await nexusDbContext.SaveChangesAsync(ct);

        GetSportDto response = new()
        {
            Id = sport.Id,
            Name = sport.Name,
            Description = sport.Description,
            Type = sport.Type,
            Place = sport.Place,
            Duration = sport.Duration,
            Intensity = sport.Intensity,
        };
        
        await Send.OkAsync(response);
    }
}