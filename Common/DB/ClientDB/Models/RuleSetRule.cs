namespace Common.DB.ClientDB.Models;

public class RuleSetRule
{
    public int Id { get; set; }

    public int Rid { get; set; }

    public int Rsid { get; set; }

    public DateTime CreateDate { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual Rule Rule { get; set; } = null!;

    public virtual RuleSet RuleSet { get; set; } = null!;
}