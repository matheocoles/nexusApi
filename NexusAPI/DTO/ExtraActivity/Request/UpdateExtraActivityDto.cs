namespace NexusAPI.DTO.ExtraActivity.Request;

public class UpdateExtraActivityDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Organiser { get; set; }
    public string? Place { get; set; }
    public string? Theme { get; set; }
    public string? Resource { get; set; }
}