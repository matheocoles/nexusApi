using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Login.Request;
using NexusAPI.DTO.Login.Response;
using PasswordGenerator;

namespace NexusAPI.Endpoints.Login;

public class UpdateLoginEndpoint(NexusDbContext database) : Endpoint<UpdateLoginDto, GetLoginDto>
{
    public override void Configure()
    {
        Put("/api/logins/{Id}"); 
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateLoginDto req, CancellationToken ct)
    {
        var login = await database.Logins.SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (login == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        login.Username = req.Username;
        login.FullName = req.FullName;

        if (!string.IsNullOrEmpty(req.Password))
        {
            string salt = new Password().IncludeLowercase().IncludeUppercase().IncludeNumeric().LengthRequired(24).Next();
            login.Salt = salt;
            login.Password = BCrypt.Net.BCrypt.HashPassword(req.Password + salt);
        }

        await database.SaveChangesAsync(ct);
        
        GetLoginDto responseDto = new()
        {
            Id = login.Id,
            Username = login.Username,
            FullName = login.FullName,
            Password = login.Password,
            Salt = login.Salt
        };
        
        await Send.OkAsync(responseDto, ct);
    }
}