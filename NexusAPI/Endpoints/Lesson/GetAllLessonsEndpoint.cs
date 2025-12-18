using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Activity.Response;
using NexusAPI.DTO.Lesson.Response;

namespace NexusAPI.Endpoints.Lesson;

public class GetAllLessonsEndpoint(NexusDbContext nexusDbContext) : EndpointWithoutRequest<List<GetLessonDto>>
{
    public override void Configure()
    {
        Get("/lesson");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        
        List<GetLessonDto> responseDto = await nexusDbContext.Lessons
            .Select(a => new GetLessonDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Subject = a.Subject,
                    Teacher = a.Teacher,
                    Room = a.Room,
                    Objective = a.Objective,
                }
            ).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }

}