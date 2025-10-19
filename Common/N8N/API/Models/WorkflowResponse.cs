using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.N8N.API.Models;

public class Workflow
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("active")] public bool Active { get; set; }

    [JsonProperty("createdAt")] public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")] public DateTime UpdatedAt { get; set; }

    [JsonProperty("nodes")] public JArray NodeData { get; set; }

    [JsonProperty("connections")] public JObject ConnectionsData { get; set; }

    [JsonProperty("settings")] public JObject SettingsData { get; set; }

    [JsonProperty("staticData")] public JObject? StaticData { get; set; }

    [JsonProperty("tags")] public List<Tag> Tags { get; set; }
}

public class WorkflowList
{
    [JsonProperty("data")] public List<Workflow> Data { get; set; }

    [JsonProperty("nextCursor")] public string NextCursor { get; set; }
}