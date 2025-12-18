using NexusAPI.DTO.Session.Request;
using NexusAPI.DTO.Session.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Session;

public class UpdateSessionEndpoint(NexusDbContext db)
    : Endpoint<UpdateSessionDto, GetSessionDto>
{
    public override void Configure()
    {
        Put("/sessions/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateSessionDto req, CancellationToken ct)
    {
        Models.Session? sessionToEdit = await db.Sessions
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (sessionToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        sessionToEdit.DateTimeStart = req.DateTimeStart;
        sessionToEdit.DateTimeEnd = req.DateTimeEnd;
        sessionToEdit.Status = req.Status;

        await db.SaveChangesAsync(ct);

        var responseDto = new GetSessionDto
        {
            Id = sessionToEdit.Id,
            DateTimeStart = sessionToEdit.DateTimeStart,
            DateTimeEnd = sessionToEdit.DateTimeEnd,
            Status = sessionToEdit.Status
        };

        await Send.OkAsync(responseDto, ct);
    }
}