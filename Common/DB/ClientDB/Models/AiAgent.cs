namespace Common.DB.ClientDB.Models;

public class AiAgent
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public int Wid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public int InstructionsId { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual Instructions Instructions { get; set; } = null!;

    public virtual Workflow Workflow { get; set; } = null!;
}