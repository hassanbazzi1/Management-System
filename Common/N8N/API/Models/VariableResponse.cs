using Newtonsoft.Json;

namespace Common.N8N.API.Models;

public class Variable
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("key")] public string Key { get; set; }

    [JsonProperty("value")] public string Value { get; set; }

    [JsonProperty("type")] public string Type { get; set; }
}

public class VariableList
{
    [JsonProperty("data")] public List<Variable> Data { get; set; }

    [JsonProperty("nextCursor")] public string NextCursor { get; set; }
}