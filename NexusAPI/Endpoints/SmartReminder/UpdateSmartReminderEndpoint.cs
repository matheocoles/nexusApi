using NexusAPI.DTO.SmartReminder.Request;
using NexusAPI.DTO.SmartReminder.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.SmartReminder;

public class UpdateSmartReminderEndpoint(NexusDbContext db)
    : Endpoint<UpdateSmartReminderDto, GetSmartReminderDto>
{
    public override void Configure()
    {
        Put("/smartreminders/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateSmartReminderDto req, CancellationToken ct)
    {
        Models.SmartReminder? smartreminderToEdit = await db.SmartReminders
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (smartreminderToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        smartreminderToEdit.Id = req.Id;
        smartreminderToEdit.DateAlert = req.DateAlert;
        smartreminderToEdit.TimeAlert = req.TimeAlert;
        smartreminderToEdit.Type = req.Type;
        smartreminderToEdit.Status = req.Status;

        await db.SaveChangesAsync(ct);

        var responseDto = new GetSmartReminderDto()
        {
            Id = smartreminderToEdit.Id,
            DateAlert = smartreminderToEdit.DateAlert,
            TimeAlert = smartreminderToEdit.TimeAlert,
            Type = smartreminderToEdit.Type,
            Status = smartreminderToEdit.Status,
        };

        await Send.OkAsync(responseDto, ct);
    }
}