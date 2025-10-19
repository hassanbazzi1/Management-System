using Common.DB.MasterDB.Models;

namespace Common.DB.ClientDB.Models;

public class Conversation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? AiAgentId { get; set; }

    public int? HumanAgentId { get; set; }

    public int? LastChatMessageId { get; set; }

    public bool IsDeleted { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual AiAgent? AiAgent { get; set; }

    public virtual HumanAgent? HumanAgent { get; set; }

    public virtual User User { get; set; } = null!;
    
    public virtual ChatMessage? LastChatMessage { get; set; }
    
    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
    
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}