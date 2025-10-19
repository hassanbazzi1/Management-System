using Newtonsoft.Json;

namespace Common.N8N.API.Models;

public class Tag
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("createdAt")] public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")] public DateTime UpdatedAt { get; set; }
}

public class TagList
{
    [JsonProperty("data")] public List<Tag> Data { get; set; }

    [JsonProperty("nextCursor")] public string? NextCursor { get; set; }
}