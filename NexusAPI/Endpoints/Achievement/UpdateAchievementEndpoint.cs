using NexusAPI.DTO.Achievement.Request;
using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Achievement;

public class UpdateAchievementEndpoint(NexusDbContext db)
    : Endpoint<UpdateAchievementDto, GetAchievementDto>
{
    public override void Configure()
    {
        Put("/achievements/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAchievementDto req, CancellationToken ct)
    {
        Models.Achievement? achievementToEdit = await db.Achievements
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (achievementToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        achievementToEdit.Name = req.Name;
        achievementToEdit.Description = req.Description;
        await db.SaveChangesAsync(ct);

        var responseDto = new GetAchievementDto()
        {
            Id = achievementToEdit.Id,
            Name = achievementToEdit.Name,
            Description = achievementToEdit.Description,
        };

        await Send.OkAsync(responseDto, ct);
    }
}