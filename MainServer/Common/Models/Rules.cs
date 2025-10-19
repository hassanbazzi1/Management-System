using Newtonsoft.Json;

namespace MainServer.Common.Models;

public class RulesContainer
{
    [JsonProperty("rulesets")] public List<Ruleset> Rulesets { get; set; }
}

public class Ruleset
{
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("rules")] public List<string> Rules { get; set; }
}