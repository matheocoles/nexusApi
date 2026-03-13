using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.EventRecurrence.Response;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.EventRecurrence;

public class GetEventRecurrenceRequest
{
    public int Id { get; set; }
}

public class GetEventRecurrenceEndpoint(NexusDbContext db)
    : Endpoint<GetEventRecurrenceRequest, GetEventRecurrenceDto>
{
    public override void Configure()
    {
        Get("/eventrecurrences/{id}");
    }

    public override async Task HandleAsync(GetEventRecurrenceRequest req, CancellationToken ct)
    {
        Models.EventRecurrence? eventRecurrence = await db.EventRecurrence
            .SingleOrDefaultAsync(e => e.Id == req.Id, ct);

        if (eventRecurrence == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var responseDto = new GetEventRecurrenceDto
        {
            Id = eventRecurrence.Id,
            Type = eventRecurrence.Type, 
            Frequency = eventRecurrence.Frequency,
            DateStart =  eventRecurrence.DateStart,
            DateEnd = eventRecurrence.DateEnd,
            Day = eventRecurrence.Day
        };

        await Send.OkAsync(responseDto, ct);
    }
}