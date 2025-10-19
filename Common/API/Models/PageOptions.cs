using System.Text.Json.Serialization;

namespace Common.API.Models;

public class PageOptions
{
    public int? LastId { get; set; }
    public int? Position { get; set; }
    public int? Limit { get; set; }
}