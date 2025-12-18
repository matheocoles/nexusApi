namespace NexusAPI.DTO.Sport.Request;

public class UpdateSportDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Place { get; set; }
    public int Duration { get; set; }
    public string? Intensity { get; set; }
}