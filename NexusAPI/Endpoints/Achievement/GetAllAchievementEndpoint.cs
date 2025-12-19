using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Achievement;

public class GetAllAchievementEndpoint(NexusDbContext db)
    : EndpointWithoutRequest<List<GetAchievementDto>>
{
    public override void Configure()
    {
        Get("/achievements");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var responseDto = await db.Achievements
            .Select(a => new GetAchievementDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
            })
            .ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}