using Common.DB.ClientDB.Models;

namespace Common.API.ApiMapping.Conversations.Models;

public class ConversationChats
{
    public Chat Chat { get; set; }
    public List<ChatMessage> ChatMessages { get; set; }
}