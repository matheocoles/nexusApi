using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Lesson;

public class DeleteLessonRequest
{
    public int Id { get; set; }
}

public class DeleteLessonEndpoint(NexusDbContext nexusDbContext) : Endpoint<DeleteLessonRequest>
{
    public override void Configure()
    {
        Delete("/lesson/{@id}", x => new { x.Id });
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(DeleteLessonRequest req, CancellationToken ct)
    {
        
        Models.Lesson? lessonToDelete = await nexusDbContext
            .Lessons
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (lessonToDelete == null)
        {
            Console.WriteLine($"Aucun cours avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        nexusDbContext.Activities.Remove(lessonToDelete);
        await nexusDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}