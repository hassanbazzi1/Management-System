using Newtonsoft.Json;

namespace MainServer.Common.Models;

public class SystemMessageWrapper
{
    [JsonProperty("system_message")] public List<SystemMessageSection> SystemMessage { get; set; }
}

public class SystemMessageSection
{
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("value")] public string Value { get; set; }
}