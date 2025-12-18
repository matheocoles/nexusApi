using NexusAPI.DTO.Session.Request;
using NexusAPI.DTO.Session.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Session;

public class CreateSessionEndpoint(NexusDbContext db)
    : Endpoint<CreateSessionDto, GetSessionDto>
{
    public override void Configure()
    {
        Post("/sessions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSessionDto req, CancellationToken ct)
    {
        var session = new Models.Session()
        {
            DateTimeEnd = req.DateTimeEnd,
            DateTimeStart = req.DateTimeStart,
            Status = req.Status,
        };
        
        db.Sessions.Add(session);
        await db.SaveChangesAsync(ct);

        var response = new GetSessionDto()
        {
            DateTimeEnd = req.DateTimeEnd,
            DateTimeStart = req.DateTimeStart,
            Status = req.Status,
        };
        
        await Send.OkAsync(response, ct);
    }
}