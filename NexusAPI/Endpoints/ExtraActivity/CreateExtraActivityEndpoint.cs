using FastEndpoints;
using NexusAPI.DTO.ExtraActivity.Request;
using NexusAPI.DTO.ExtraActivity.Response;

namespace NexusAPI.Endpoints.ExtraActivity;

public class CreateExtraActivityEndpoint(NexusDbContext nexusDbContext) : Endpoint<CreateExtraActivityDto, GetExtraActivityDto>
{
    public override void Configure()
    {
        Post("/extraactivity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateExtraActivityDto req, CancellationToken ct)
    {
        Models.ExtraActivity extraactivity = new()
        {
            Name = req.Name,
            Description = req.Description,
            Organiser = req.Organiser,
            Place = req.Place,
            Theme =  req.Theme,
            Resource = req.Resource,
            
        };
        
        nexusDbContext.ExtraActivities.Add(extraactivity);
        await nexusDbContext.SaveChangesAsync(ct);
        
        Console.WriteLine($"Created extraActivity {extraactivity.Name}");

        GetExtraActivityDto activityDto = new()
        {
            Name = extraactivity.Name,
            Description = extraactivity.Description,
            Organiser = extraactivity.Organiser,
            Place = extraactivity.Place,
            Theme = extraactivity.Theme,
            Resource = extraactivity.Resource,
        };
        
        await nexusDbContext.AddAsync(activityDto, ct);
    }
}