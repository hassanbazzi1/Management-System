namespace Common.API.ApiMapping.Conversations.Models;

public class ChatResponse
{
    public int Id { get; set; }
    
    public int ConversationId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    
    public int? EscalationId { get; set; }
    
    public DateTime CreateDate { get; set; }
}