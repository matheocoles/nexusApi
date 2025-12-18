using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using NexusAPI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = "ThisIsASuperSecretJwtKeyThatIsAtLeast32CharsLong")
    .AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policyBuilder =>
        policyBuilder.WithOrigins("http://localhost:4200")
            .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
            .AllowAnyHeader()
            .AllowCredentials() 
    )
);

builder.Services.AddFastEndpoints().SwaggerDocument(options => 
{
    options.ShortSchemaNames = true;
});

builder.Services.AddDbContext<NexusDbContext>();

WebApplication app = builder.Build();


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "API"; 
    options.Endpoints.ShortNames = true;
}).UseSwaggerGen();

app.Run();