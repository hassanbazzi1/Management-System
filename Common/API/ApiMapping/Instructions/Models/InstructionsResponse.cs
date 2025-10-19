namespace Common.API.ApiMapping.Instructions.Models;

public class InstructionsResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }
}