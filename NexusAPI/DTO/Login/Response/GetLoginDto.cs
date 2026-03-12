using FastEndpoints;

namespace NexusAPI.DTO.Login.Response;

public class GetLoginDto
{
    [BindFrom("id")]
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
}