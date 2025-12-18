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
        
        List<GetActivityDto> responseDto = await nexusDbContext.Activities
            .Select(a => new GetActivityDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                }
            ).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }

}