using NexusAPI.DTO.Session.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;


namespace NexusAPI.Endpoints.Session;

public class GetSessionRequest
{
    public int Id { get; set; }
}

public class GetSessionEndpoint(NexusDbContext db)
    : Endpoint<GetSessionRequest, GetSessionDto>
{
    public override void Configure()
    {
    Get("/sessions/{@id}", x => new { x.Id });
    AllowAnonymous();
    }

    public override async Task HandleAsync(GetSessionRequest req, CancellationToken ct)
    {
        Models.Session? session = await db
            .Sessions
            .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (session == null)
        {
            Console.WriteLine("Aucun matériel avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetSessionDto responseDto = new()
        {
            Id = session.Id,
            DateTimeStart = session.DateTimeStart,
            DateTimeEnd = session.DateTimeEnd,
            Status = session.Status
        };
        await Send.OkAsync(responseDto, ct);

    }
}