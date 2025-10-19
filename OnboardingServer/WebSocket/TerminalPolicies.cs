using System.Collections.Concurrent;
using Common.SignalR;

namespace SignupService.WebSocket;

/// <summary>
///     Manages terminal sessions and policies, including handshake, authentication status, and connection updates.
/// </summary>
public class TerminalPolicies
{
    private readonly ILogger<TerminalPolicies> _logger;

    public TerminalPolicies(ILogger<TerminalPolicies> logger)
    {
        _logger = logger;
    }

    public ConcurrentDictionary<string, TerminalSessionInfo> Terminals { get; } = new();

    public string OnHandshake(ClientConnectionInfo clientInfo)
    {
        var newSession = new TerminalSessionInfo
        {
            TerminalId = clientInfo.TerminalId,
            ConnectionId = clientInfo.ConnectionId,
            LastHandshake = DateTime.Now,
            IsAuthenticated = false,
            ClientInfo = clientInfo
        };

        Terminals[clientInfo.TerminalId] = newSession;

        return string.Empty;
    }

    /// <summary>
    ///     Marks the specified terminal session as authenticated.
    /// </summary>
    public void MarkTerminalAsAuthenticated(string terminalId)
    {
        if (Terminals.TryGetValue(terminalId, out var session))
            session.IsAuthenticated = true;
    }

    /// <summary>
    ///     Removes the terminal session and forces logout of any existing session.
    /// </summary>
    public void RemoveTerminal(string terminalId)
    {
        if (Terminals.TryRemove(terminalId, out var session))
            _logger.LogInformation("Removed terminal {TerminalId}, hub id {ConnectionId}", terminalId, session.ConnectionId);
        //ForceLogoutOldSession(session);
    }

    private void ForceLogoutOldSession(TerminalSessionInfo oldSession)
    {
        // TODO: Notify RMQ to force logout the old session user.
        //_rmqProducer.SendUserForceLogout(oldSession.UserName, oldSession.TerminalId);
    }

    /// <summary>
    ///     Gets the client connection info for the specified terminal.
    /// </summary>
    public ClientConnectionInfo? GetClientConnection(string terminalId)
    {
        return Terminals.TryGetValue(terminalId, out var session) ? session.ClientInfo : null;
    }

    /// <summary>
    ///     Finds the terminal ID associated with the given hub connection ID.
    /// </summary>
    public string? GetClientTerminalIdByConnectionId(string hubConnectionId)
    {
        return Terminals.Values.FirstOrDefault(s => s.ClientInfo.ConnectionId == hubConnectionId)?.TerminalId;
    }
}

/// <summary>
///     Represents information about a terminal's session.
/// </summary>
public record TerminalSessionInfo
{
    public string TerminalId { get; init; }
    public string ConnectionId { get; set; }
    public bool IsAuthenticated { get; set; }
    public DateTime LastHandshake { get; set; }
    public ClientConnectionInfo ClientInfo { get; set; }
}