namespace Common.DB.ClientDB.Models;

public class Instructions
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<AiAgent> AiAgents { get; set; } = new List<AiAgent>();

    public virtual ICollection<InstructionsRule> InstructionsRules { get; set; } = new List<InstructionsRule>();

    public virtual ICollection<InstructionsSection> InstructionsSections { get; set; } = new List<InstructionsSection>();
}