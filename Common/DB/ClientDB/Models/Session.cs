namespace Common.DB.ClientDB.Models;

public class Session
{
    public int Id { get; set; }

    public int ConversationId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual Conversation Conversation { get; set; }
}