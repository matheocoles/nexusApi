using FastEndpoints;
using NexusAPI.DTO.Class.Request;
using NexusAPI.DTO.Class.Response;


namespace NexusAPI.Endpoints.Class;

public class UpdateClassEndpoint(NexusDbContext nexusDbContext) : Endpoint<UpdateClassDto, GetClassDto>
{
    public override void Configure()
    {
        Put("/class");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateClassDto req, CancellationToken ct)
    {
        Models.Class @class = new()
        {
            Name = req.Name,
            Description = req.Description, 
            Subject =  req.Subject,
            Teacher = req.Teacher,
            Room = req.Room,
            Objective = req.Objective,
        };
        
        nexusDbContext.Classes.Update(@class);
        await nexusDbContext.SaveChangesAsync(ct);

        GetClassDto response = new()
        {
            Id = @class.Id,
            Name = @class.Name,
            Description = @class.Description,
            Subject = @class.Subject,
            Teacher = @class.Teacher,
            Room = @class.Room,
            Objective = @class.Objective,
        };
        
        await Send.OkAsync(response);
    }
}