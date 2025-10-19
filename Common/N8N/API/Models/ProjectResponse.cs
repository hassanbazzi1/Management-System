using Newtonsoft.Json;

namespace Common.N8N.API.Models;

public class Project
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("type")] public string Type { get; set; }
}

public class ProjectList
{
    [JsonProperty("data")] public List<Project> Data { get; set; }

    [JsonProperty("nextCursor")] public string NextCursor { get; set; }
}