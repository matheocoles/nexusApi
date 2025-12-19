using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Login.Request;
using NexusAPI.DTO.Login.Response;
using PasswordGenerator;

namespace NexusAPI.Endpoints.Login;

public class CreateLoginEndpoint(NexusDbContext database) : Endpoint<CreateLoginDto, GetLoginDto>
{
    public override void Configure()
    {
        Post("/logins");
        Roles("Admin");
    }

    public override async Task HandleAsync(CreateLoginDto req, CancellationToken ct)
    {
        bool exists = await database.Logins.AnyAsync(x => x.Username == req.Username, ct);
        if (exists)
        {
            AddError("Ce nom d'utilisateur est déjà utilisé.");
            await Send.ErrorsAsync(400, ct);
            return;
        }

        string? salt = new Password().IncludeLowercase().IncludeUppercase().IncludeNumeric().LengthRequired(24).Next();
        
        Models.Login login = new Models.Login()
        {
            Username = req.Username,
            FullName = req.FullName,
            Password = BCrypt.Net.BCrypt.HashPassword(req.Password + salt),
            Salt = salt,
            
            Role = string.IsNullOrEmpty(req.Role) ? "User" : req.Role
        };
        
        database.Logins.Add(login);
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