namespace Common.API.ApiMapping.Conversations.Models;

public class ConversationChatsResponse
{
    public ChatResponse Chat { get; set; }
    public List<ChatMessageResponse> ChatMessages { get; set; }
}