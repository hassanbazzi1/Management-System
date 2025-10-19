namespace Common.API.ApiMapping.Conversations.Models;

public class ChatMessageResponse
{
    public int Id { get; set; }

    public int Cid { get; set; }

    public DateTime Date { get; set; }

    public bool IsUser { get; set; }

    public string Text { get; set; } = null!;
}