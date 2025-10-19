namespace Common.DB.ClientDB.Models;

public class Rule
{
    public string Text { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public string Name { get; set; } = null!;

    public int Id { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<InstructionsRule> InstructionsRules { get; set; } = new List<InstructionsRule>();

    public virtual ICollection<RuleSetRule> RuleSetRules { get; set; } = new List<RuleSetRule>();
}