public class ProjectDto
{
    public Guid? ProjectID { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Tech { get; set; }  // comma-separated
    public Guid? ServiceID { get; set; }
    public string? CategoryName { get; set; }
}
