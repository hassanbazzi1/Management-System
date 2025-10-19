namespace Common.DB.ClientDB.Models;

public class Workflow
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

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<AiAgent> AiAgents { get; set; } = new List<AiAgent>();

    public virtual WorkflowTemplate WorkflowTemplate { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual Subscription Subscription { get; set; } = null!;
}