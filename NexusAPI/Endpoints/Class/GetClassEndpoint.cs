using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Class.Response;

namespace NexusAPI.Endpoints.Class;

public class GetClassRequest
{
    public int Id { get; set; }
}

public class GetClassEndpoint(NexusDbContext nexusDbContext) :Endpoint<GetClassRequest, GetClassDto>
{
    public override void Configure()
    {
        Get("/class/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetClassRequest req, CancellationToken ct)
    {
        
        Models.Class? @class = await nexusDbContext
            .Classes
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (@class == null)
        {
            Console.WriteLine($"Aucun cours avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetClassDto responseDto = new()
        {
            Id = req.Id, 
            Name = @class.Name,
            Description = @class.Description,
            Subject =  @class.Subject,
            Teacher =  @class.Teacher,
            Room = @class.Room,
            Objective = @class.Objective,
        };

        await Send.OkAsync(responseDto, ct);
    }
}