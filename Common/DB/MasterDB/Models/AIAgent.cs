namespace Common.DB.MasterDB.Models;

public class AiAgent
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public int InstructionsId { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual AiAgentInstructions AiAgentInstructions { get; set; } = null!;
}