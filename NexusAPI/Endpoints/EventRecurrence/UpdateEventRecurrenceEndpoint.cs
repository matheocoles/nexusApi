using FastEndpoints;
using NexusAPI.DTO.EventRecurrence.Request;
using NexusAPI.DTO.EventRecurrence.Response;

namespace NexusAPI.Endpoints.EventRecurrence;

public class UpdateEventRecurrenceEndpoint(NexusDbContext nexusDbContext) : Endpoint<UpdateEventRecurrenceDto, GetEventRecurrenceDto>
{
    public override void Configure()
    {
        Put("/eventrecurrences");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateEventRecurrenceDto req, CancellationToken ct)
    {
        Models.EventRecurrence eventRecurrence = new()
        {
            Type = req.Type, 
            Frequency = req.Frequency,
            DateStart = req.DateStart,
            DateEnd = req.DateEnd,
            StartTime = req.StartTime,
            EndTime = req.EndTime,
            Day = req.Day
        };
        
        nexusDbContext.EventRecurrence.Update(eventRecurrence);
        await nexusDbContext.SaveChangesAsync(ct);

        GetEventRecurrenceDto response = new()
        {
            Id = eventRecurrence.Id,
            Type = eventRecurrence.Type, 
            Frequency = eventRecurrence.Frequency,
            DateEnd = eventRecurrence.DateEnd,
            StartTime =  eventRecurrence.StartTime,
            EndTime =  eventRecurrence.EndTime,
            Day = eventRecurrence.Day
        };
        
        await Send.OkAsync(response);
    }
}