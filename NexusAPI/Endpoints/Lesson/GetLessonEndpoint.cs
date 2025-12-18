using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Lesson.Response;

namespace NexusAPI.Endpoints.Lesson;

public class GetLessonRequest
{
    public int Id { get; set; }
}

public class GetLessonEndpoint(NexusDbContext nexusDbContext) :Endpoint<GetLessonRequest, GetLessonDto>
{
    public override void Configure()
    {
        Get("/lesson/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetLessonRequest req, CancellationToken ct)
    {
        
        Models.Lesson? lesson = await nexusDbContext
            .Lessons
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (lesson == null)
        {
            Console.WriteLine($"Aucun cours avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetLessonDto responseDto = new()
        {
            Id = req.Id, 
            Name = lesson.Name,
            Description = lesson.Description,
            Subject =  lesson.Subject,
            Teacher =  lesson.Teacher,
            Room = lesson.Room,
            Objective = lesson.Objective,
        };

        await Send.OkAsync(responseDto, ct);
    }
}