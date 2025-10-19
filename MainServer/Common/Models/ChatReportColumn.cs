using Newtonsoft.Json;

namespace MainServer.Common.Models;

public class Column
{
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("description")] public string Description { get; set; }
}

public class ColumnContainer
{
    [JsonProperty("columns")] public List<Column> Columns { get; set; }
}