namespace Common.API.ApiMapping.Conversations.Models;

public class ConversationResponse
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? AiAgentId { get; set; }

    public int? HumanAgentId { get; set; }

    public ChatMessageResponse LastChatMessage { get; set; } = null!;
    
    public DateTime CreateDate { get; set; }
    
}