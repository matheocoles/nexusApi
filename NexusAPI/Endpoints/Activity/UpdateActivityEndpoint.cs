using FastEndpoints;
using NexusAPI.DTO.Activity.Request;
using NexusAPI.DTO.Activity.Response;

namespace NexusAPI.Endpoints.Activity;

public class UpdateActivityEndpoint(NexusDbContext nexusDbContext) : Endpoint<UpdateActivityDto, GetActivityDto>
{
    public override void Configure()
    {
        Put("/activity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateActivityDto req, CancellationToken ct)
    {
        Models.Activity activity = new()
        {
            Name = req.Name,
            Description = req.Description, 
        };
        
        nexusDbContext.Activities.Update(activity);
        await nexusDbContext.SaveChangesAsync(ct);

        GetActivityDto response = new()
        {
            Id = activity.Id,
            Name = activity.Name,
            Description = activity.Description,
        };
        
        await Send.OkAsync(response);
    }
}