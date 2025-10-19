namespace Common.SignalR;

public class ClientConnectionInfo
{
    public string TerminalId { get; set; }
    public string ConnectionId { get; set; }
    public bool IsAuthenticated { get; set; }
    public DateTime LastHandshake { get; set; }
}