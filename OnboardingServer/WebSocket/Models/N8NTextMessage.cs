namespace SignupService.WebSocket.Models;

public class N8NTextMessage
{
    public string Kind { get; set; }

    public string Data { get; set; }

    public string Type { get; set; }

    public string ConnectionId { get; set; }
}