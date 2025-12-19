using FastEndpoints;
using NexusAPI.DTO.Activity.Request;
using NexusAPI.DTO.Activity.Response;
using NexusAPI.DTO.ExtraActivity.Request;
using NexusAPI.DTO.ExtraActivity.Response;

namespace NexusAPI.Endpoints.ExtraActivity;

public class UpdateExtraActivityEndpoint(NexusDbContext nexusDbContext) : Endpoint<UpdateExtraActivityDto, GetExtraActivityDto>
{
    public override void Configure()
    {
        Put("/extraactivity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateExtraActivityDto req, CancellationToken ct)
    {
        Models.ExtraActivity extraActivity = new()
        {
            Name = req.Name,
            Description = req.Description, 
            Resource = req.Resource,
            Organiser = req.Organiser,
            Place = req.Place,
            Theme = req.Theme,
        };
        
        nexusDbContext.ExtraActivities.Update(extraActivity);
        await nexusDbContext.SaveChangesAsync(ct);

        GetExtraActivityDto response = new()
        {
            Id = extraActivity.Id,
            Name = extraActivity.Name,
            Description = extraActivity.Description,
            Resource = extraActivity.Resource,
            Organiser = extraActivity.Organiser,
            Place = extraActivity.Place,
            Theme = extraActivity.Theme,
        };
        
        await Send.OkAsync(response);
    }
}