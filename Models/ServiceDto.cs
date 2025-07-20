public class ServiceDto
{
    public Guid? ServiceID { get; set; }
    public string? Icon { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Features { get; set; }  // This is the raw comma-separated string
    public string? Color { get; set; }
}
