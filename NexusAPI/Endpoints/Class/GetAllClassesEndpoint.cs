using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Class.Response;

namespace NexusAPI.Endpoints.Class;

public class GetAllClassesEndpoint(NexusDbContext nexusDbContext) : EndpointWithoutRequest<List<GetClassDto>>
{
    public override void Configure()
    {
        Get("/class");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        
        List<GetClassDto> responseDto = await nexusDbContext.Classes
            .Select(a => new GetClassDto()
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