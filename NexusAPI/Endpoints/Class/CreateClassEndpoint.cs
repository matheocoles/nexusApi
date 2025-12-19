using FastEndpoints;
using NexusAPI.DTO.Class.Request;
using NexusAPI.DTO.Class.Response;

namespace NexusAPI.Endpoints.Class;

public class CreateClassEndpoint(NexusDbContext nexusDbContext) : Endpoint<CreateClassDto, GetClassDto>
{
    public override void Configure()
    {
        Post("/class");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateClassDto req, CancellationToken ct)
    {
        Models.Class @class = new()
        {
            Name = req.Name,
            Description = req.Description,
            Subject = req.Subject,
            Teacher = req.Teacher,
            Room = req.Room,
            Objective = req.Objective,
        };
        
        nexusDbContext.Activities.Add(@class);
        await nexusDbContext.SaveChangesAsync(ct);
        
        Console.WriteLine($"Created class ");

        GetClassDto classDto = new()
        {
            Name = @class.Name,
            Description = @class.Description,
            Subject = @class.Subject,
            Teacher = @class.Teacher,
            Room = @class.Room,
            Objective = @class.Objective,
        };
        
        await nexusDbContext.AddAsync(classDto, ct);
    }
}