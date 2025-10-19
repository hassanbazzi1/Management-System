namespace Common.DB.ClientDB.Models;

public class ChatEscalationStatus
{
    public int Id { get; set; }
    
    public string Code { get; set; } = null!;
    
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    
    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<ChatEscalation> ChatEscalations { get; set; } = new List<ChatEscalation>();
}