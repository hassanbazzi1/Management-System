namespace Common.API.ApiMapping.Industries.Models;

public class IndustryResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDefault { get; set; }

    public DateTime CreateDate { get; set; }
}