using NexusAPI.DTO.SmartReminder.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;


namespace NexusAPI.Endpoints.SmartReminder;

public class GetSmartReminderRequest
{
    public int Id { get; set; }
}

public class GetSessionEndpoint(NexusDbContext db)
    : Endpoint<GetSmartReminderRequest, GetSmartReminderDto>
{
    public override void Configure()
    {
        Get("/smartreminders/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSmartReminderRequest req, CancellationToken ct)
    {
        Models.SmartReminder? smartReminder = await db
            .SmartReminders
            .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (smartReminder == null)
        {
            Console.WriteLine("Aucun rappel intelligent avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetSmartReminderDto responseDto = new()
        {
            Id = smartReminder.Id,
            DateAlert = smartReminder.DateAlert,
            TimeAlert = smartReminder.TimeAlert,
            Type = smartReminder.Type,
            Status = smartReminder.Status,
        };
        await Send.OkAsync(responseDto, ct);

    }
}