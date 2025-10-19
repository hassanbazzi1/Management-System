namespace Common.API.ApiMapping.Workflows.Models;

public class WorkflowTemplateResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public string? Json { get; set; }
}