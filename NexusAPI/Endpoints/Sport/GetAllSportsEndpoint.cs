using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Activity.Response;
using NexusAPI.DTO.Sport.Response;

namespace NexusAPI.Endpoints.Sport;

public class GetAllASportsEndpoint(NexusDbContext nexusDbContext) : EndpointWithoutRequest<List<GetSportDto>>
{
    public override void Configure()
    {
        Get("/sport");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        
        List<GetSportDto> responseDto = await nexusDbContext.Sports
            .Select(a => new GetSportDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                }
            ).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }

}