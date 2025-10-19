namespace Common.DB.MasterDB.Models;

public class AiAgentInstructions
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Text { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<AiAgent> AiAgents { get; set; } = new List<AiAgent>();
}