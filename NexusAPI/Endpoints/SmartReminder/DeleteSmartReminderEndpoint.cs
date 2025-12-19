using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.SmartReminder;

public class DeleteSmartReminderRequest
{
    public int Id { get; set; }
}

public class DeleteSmartReminderEndpoint(NexusDbContext db)
    : Endpoint<DeleteSmartReminderRequest>
{
    public override void Configure()
    {
        Delete("/smartreminders/{@id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteSmartReminderRequest req, CancellationToken ct)
    {
        var smartReminderToDelete = await db.SmartReminders
            .SingleOrDefaultAsync(p => p.Id == req.Id, ct);

        if (smartReminderToDelete is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        db.SmartReminders.Remove(smartReminderToDelete);
        await db.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}