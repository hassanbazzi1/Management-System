using Newtonsoft.Json;

namespace MainServer.Common.Models;

public class ColumnType
{
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("description")] public string Description { get; set; }
}

public class ColumnTypeContainer
{
    [JsonProperty("columnTypes")] public List<ColumnType> ColumnTypes { get; set; }
}