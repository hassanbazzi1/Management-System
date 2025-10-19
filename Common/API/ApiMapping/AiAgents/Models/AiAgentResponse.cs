namespace Common.API.ApiMapping.AiAgents.Models;

public class AiAgentResponse
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public int Wid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public int InstructionsId { get; set; }
}