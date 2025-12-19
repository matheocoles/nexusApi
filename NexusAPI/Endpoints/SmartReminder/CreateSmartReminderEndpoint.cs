using NexusAPI.DTO.SmartReminder.Request;
using NexusAPI.DTO.SmartReminder.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.SmartReminder;

public class CreateSmartReminderEndpoint(NexusDbContext db)
    : Endpoint<CreateSmartReminderDto, GetSmartReminderDto>
{
    public override void Configure()
    {
        Post("/smartreminders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSmartReminderDto req, CancellationToken ct)
    {
        var smartreminder = new Models.SmartReminder()
        {
            DateAlert = req.DateAlert,
            TimeAlert = req.TimeAlert,
            Type = req.Type,
            Status = req.Status,
        };
        
        db.SmartReminders.Add(smartreminder);
        await db.SaveChangesAsync(ct);

        var response = new GetSmartReminderDto()
        {
            DateAlert = req.DateAlert,
            TimeAlert = req.TimeAlert,
            Type = req.Type,
            Status = req.Status,
        };
        
        await Send.OkAsync(response, ct);
    }
}