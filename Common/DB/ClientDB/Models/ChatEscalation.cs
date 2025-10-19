namespace Common.DB.ClientDB.Models;

public class ChatEscalation
{
    public int Id { get; set; }

    public int SourceChatId { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public int StatusId { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual Chat Chat { get; set; }
    
    public virtual ChatEscalationStatus ChatEscalationStatus { get; set; }
}