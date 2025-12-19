using NexusAPI.DTO.Session.Response;
using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Session;

public class GetAllSessionEndpoint(NexusDbContext db)
    : EndpointWithoutRequest<List<GetSessionDto>>
{
    public override void Configure()
    {
        Get("/sessions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var responseDto = await db.Sessions
            .Include(s => s.SessionAchievements)
            .ThenInclude(sa => sa.Achievement)
            .Select(s => new GetSessionDto
            {
                Id = s.Id,
                DateTimeStart = s.DateTimeStart,
                DateTimeEnd = s.DateTimeEnd,
                Status = s.Status,
                Achievements = s.SessionAchievements
                    .Select(sa => new GetAchievementDto
                    {
                        Id = sa.Achievement.Id,
                        Name = sa.Achievement.Name,
                        Description = sa.Achievement.Description
                    })
                    .ToList()
            })
            .ToListAsync<GetSessionDto>(ct);

        await Send.OkAsync(responseDto, ct);
    }
}