using Newtonsoft.Json;

namespace Common.N8N.API.Models;

public class User
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("email")] public string Email { get; set; }

    [JsonProperty("firstName")] public string FirstName { get; set; }

    [JsonProperty("lastName")] public string LastName { get; set; }

    [JsonProperty("isPending")] public bool IsPending { get; set; }

    [JsonProperty("createdAt")] public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")] public DateTime UpdatedAt { get; set; }

    [JsonProperty("role")] public string Role { get; set; }
}

public class UserList
{
    [JsonProperty("data")] public List<User> Data { get; set; }

    [JsonProperty("nextCursor")] public string? NextCursor { get; set; }
}