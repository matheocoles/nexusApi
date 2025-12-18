namespace NexusAPI.DTO.Lesson.Response;

public class GetLessonDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Subject { get; set; }
    public string? Teacher { get; set; }
    public string? Room { get; set; }
    public string? Objective { get; set; }
}