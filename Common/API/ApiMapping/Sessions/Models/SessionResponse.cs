namespace Common.API.ApiMapping.Sessions.Models;

public class SessionResponse
{
    public int Id { get; set; }

    public int Uid { get; set; }

    public int? AiAgentId { get; set; }

    public int? HumanAgentId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}