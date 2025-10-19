namespace Common.API.ApiMapping.Workflows.Models;

public class WorkflowResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Url { get; set; }

    public DateTime CreateDate { get; set; }

    public int TemplateId { get; set; }

    public int ProjectId { get; set; }

    public int SubscriptionId { get; set; }

    public string? Json { get; set; }
}