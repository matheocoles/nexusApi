using FastEndpoints;
using NexusAPI.DTO.EventRecurrence.Request;
using NexusAPI.DTO.EventRecurrence.Response;

namespace NexusAPI.Endpoints.EventRecurrence;

public class CreateEventRecurrenceEndpoint(NexusDbContext db)
    : Endpoint<CreateEventRecurrenceDto, GetEventRecurrenceDto>
{
    public override void Configure()
    {
        Post("/eventrecurrences");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateEventRecurrenceDto req, CancellationToken ct)
    {
        var eventRecurrence = new Models.EventRecurrence
        {
            Type = req.Type,
            Frequency = req.Frequency,
            DateStart = req.DateStart,
            DateEnd = req.DateEnd,
            Day = req.Day
        };

        db.EventRecurrence.Add(eventRecurrence);
        await db.SaveChangesAsync(ct);

        var responseDto = new GetEventRecurrenceDto
        {
            Id = eventRecurrence.Id,
            Type      = eventRecurrence.Type.ToString(), 
            Frequency = eventRecurrence.Frequency,
            DateStart = eventRecurrence.DateStart,
            DateEnd = eventRecurrence.DateEnd,
            Day = eventRecurrence.Day
        };

        await Send.OkAsync(responseDto, cancellation: ct);
    }
}