namespace Common.DB.ClientDB.Models;

public class RuleSet
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public int Id { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<RuleSetRule> RuleSetRules { get; set; } = new List<RuleSetRule>();
}