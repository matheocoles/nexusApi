using NexusAPI.DTO.Achievement.Request;
using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Achievement;

public class CreateAchievementEndpoint(NexusDbContext db)
    : Endpoint<CreateAchievementDto, GetAchievementDto>
{
    public override void Configure()
    {
        Post("achievements");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAchievementDto req, CancellationToken ct)
    {
        var achievement = new Models.Achievement()
        {
            Name = req.Name,
            Description = req.Description,
        };
        
        db.Achievements.Add(achievement);
        await db.SaveChangesAsync(ct);

        var response = new GetAchievementDto()
        {
            Name = req.Name,
            Description = req.Description,
        };
        
        await Send.OkAsync(response, ct);
    }
}