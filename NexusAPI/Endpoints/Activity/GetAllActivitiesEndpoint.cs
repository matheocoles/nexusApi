using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Activity.Response;

namespace NexusAPI.Endpoints.Activity;

public class GetAllActivitiesEndpoint(NexusDbContext nexusDbContext) : EndpointWithoutRequest<List<GetActivityDto>>
{
    public override void Configure()
    {
        Get("/activity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var activities = await nexusDbContext.Activities.ToListAsync(ct);

        var responseDto = activities.Select(a => new GetActivityDto
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            StartTime = a.DateTimeStart, 
            EndTime = a.DateTimeEnd,
        
            Room = a switch
            {
                NexusAPI.Models.Class c => c.Room,
                NexusAPI.Models.Sport s => s.Place,
                NexusAPI.Models.ExtraActivity e => e.Place,
                _ => "Nexus Zone"
            },

            TypeLabel = a switch
            {
                NexusAPI.Models.Class _ => "Cours",
                NexusAPI.Models.Sport _ => "Sport",
                NexusAPI.Models.ExtraActivity _ => "Extra",
                _ => "Activité"
            }
        }).ToList();

        await Send.OkAsync(responseDto, cancellation: ct);
    }
}