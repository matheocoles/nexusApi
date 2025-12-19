using FastEndpoints;
using NexusAPI.DTO.Lesson.Request;
using NexusAPI.DTO.Lesson.Response;

namespace NexusAPI.Endpoints.Lesson;

public class CreateLessonEndpoint(NexusDbContext nexusDbContext) : Endpoint<CreateLessonDto, GetLessonDto>
{
    public override void Configure()
    {
        Post("/lesson");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateLessonDto req, CancellationToken ct)
    {
        Models.Lesson lesson = new()
        {
            Name = req.Name,
            Description = req.Description,
            Subject = req.Subject,
            Teacher = req.Teacher,
            Room = req.Room,
            Objective = req.Objective,
        };
        
        nexusDbContext.Activities.Add(lesson);
        await nexusDbContext.SaveChangesAsync(ct);
        
        Console.WriteLine($"Created lesson ");

        GetLessonDto lessonDto = new()
        {
            Name = lesson.Name,
            Description = lesson.Description,
            Subject = lesson.Subject,
            Teacher = lesson.Teacher,
            Room = lesson.Room,
            Objective = lesson.Objective,
        };
        
        await nexusDbContext.AddAsync(lessonDto, ct);
    }
}