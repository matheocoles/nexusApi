using FastEndpoints;
using NexusAPI.DTO.Lesson.Request;
using NexusAPI.DTO.Lesson.Response;

namespace NexusAPI.Endpoints.Lesson;

public class UpdateActivityEndpoint(NexusDbContext nexusDbContext) : Endpoint<UpdateLessonDto, GetLessonDto>
{
    public override void Configure()
    {
        Put("/lesson");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateLessonDto req, CancellationToken ct)
    {
        Models.Lesson lesson = new()
        {
            Name = req.Name,
            Description = req.Description, 
            Subject =  req.Subject,
            Teacher = req.Teacher,
            Room = req.Room,
            Objective = req.Objective,
        };
        
        nexusDbContext.Lessons.Update(lesson);
        await nexusDbContext.SaveChangesAsync(ct);

        GetLessonDto response = new()
        {
            Id = lesson.Id,
            Name = lesson.Name,
            Description = lesson.Description,
            Subject = lesson.Subject,
            Teacher = lesson.Teacher,
            Room = lesson.Room,
            Objective = lesson.Objective,
        };
        
        await Send.OkAsync(response);
    }
}