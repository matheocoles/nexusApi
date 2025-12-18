using NexusAPI.DTO.Session.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Session;

public class GetAllSessionEndpoint(NexusDbContext db)
    : EndpointWithoutRequest<List<GetSessionDto>>
{
    public override void Configure()
    {
        Get("/sessions");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var responseDto = await db.Sessions
            .Select(a => new GetSessionDto
            {
                Id = a.Id,
                DateTimeStart = a.DateTimeStart,
                DateTimeEnd = a.DateTimeEnd,
                Status = a.Status
            })
            .ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}