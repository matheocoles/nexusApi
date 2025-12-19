using NexusAPI.DTO.SmartReminder.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Session.Response;

namespace NexusAPI.Endpoints.Session;

public class GetAllSmartReminderEndpoint(NexusDbContext db)
    : EndpointWithoutRequest<List<GetSmartReminderDto>>
{
    public override void Configure()
    {
        Get("/smartreminders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var responseDto = await db.SmartReminders
            .Select(a => new GetSmartReminderDto
            {
                Id = a.Id,
                DateAlert = a.DateAlert,
                TimeAlert = a.TimeAlert,
                Type = a.Type,
                Status = a.Status,
            })
            .ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}