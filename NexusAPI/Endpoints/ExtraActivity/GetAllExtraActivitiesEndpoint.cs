using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Activity.Response;
using NexusAPI.DTO.ExtraActivity.Response;

namespace NexusAPI.Endpoints.ExtraActivity;

public class GetAllExtraActivitiesEndpoint(NexusDbContext nexusDbContext) : EndpointWithoutRequest<List<GetExtraActivityDto>>
{
    public override void Configure()
    {
        Get("/extraactivity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        
        List<GetExtraActivityDto> responseDto = await nexusDbContext.ExtraActivities
            .Select(a => new GetExtraActivityDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Organiser =  a.Organiser,
                    Place = a.Place,
                    Theme = a.Theme,
                    Resource = a.Resource,
                }
            ).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }

}