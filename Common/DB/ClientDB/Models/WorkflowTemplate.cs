namespace Common.DB.ClientDB.Models;

public class WorkflowTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public string? Json { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}