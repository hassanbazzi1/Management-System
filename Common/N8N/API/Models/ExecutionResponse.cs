using Newtonsoft.Json;

namespace Common.N8N.API.Models;

public class ExecutionList
{
    [JsonProperty("data")] public List<Execution> Data { get; set; }

    [JsonProperty("nextCursor")] public string NextCursor { get; set; }
}

public class Execution
{
    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("data")] public string Data { get; set; }

    [JsonProperty("finished")] public bool IsFinished { get; set; }

    [JsonProperty("mode")] public string Mode { get; set; }

    [JsonProperty("retryOf")] public int RetryOf { get; set; }

    [JsonProperty("retrySuccessId")] public int RetrySuccessId { get; set; }

    [JsonProperty("startedAt")] public DateTime StartedAt { get; set; }

    [JsonProperty("stoppedAt")] public DateTime StoppedAt { get; set; }

    [JsonProperty("workflowId")] public int WorkflowId { get; set; }

    [JsonProperty("waitTill")] public DateTime? WaitTill { get; set; }

    [JsonProperty("customData")] public string CustomData { get; set; }
}