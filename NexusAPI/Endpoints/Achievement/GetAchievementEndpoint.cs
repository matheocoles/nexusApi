using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;


namespace NexusAPI.Endpoints.Achievement;

public class GetAchievementRequest
{
    public int Id { get; set; }
}

public class getAchievementRequest(NexusDbContext db)
    : Endpoint<GetAchievementRequest, GetAchievementDto>
{
    public override void Configure()
    {
        Get("/achievements/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAchievementRequest req, CancellationToken ct)
    {
        Models.Achievement? achievement = await db
            .Achievements
            .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (achievement == null)
        {
            Console.WriteLine("Aucun trophée avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetAchievementDto responseDto = new()
        {
            Id = achievement.Id,
            Name = achievement.Name,
            Description = achievement.Description,
        };
        await Send.OkAsync(responseDto, ct);

    }
}