namespace Common.API.ApiMapping.Rules.Models;

public class RuleResponse
{
    public string Text { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public string Name { get; set; } = null!;

    public int Id { get; set; }
}