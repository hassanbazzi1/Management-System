namespace Common.DB.ClientDB.Models;

public class Chat
{
    public int Id { get; set; }
    
    public int ConversationId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public int? EscalationId { get; set; }
    
    public uint Xmin { get; set; } 
    
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual ICollection<ChatReport> ChatReports { get; set; } = new List<ChatReport>();

    public virtual Conversation Conversation { get; set; } = null!;
    
    public virtual ChatEscalation ChatEscalation { get; set; } = null!;

}