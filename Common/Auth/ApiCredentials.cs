namespace Common.Auth;

public class ApiCredentials
{
    public ApiCredentials(string keyName, string apiKey)
    {
        KeyName = keyName;
        ApiKey = apiKey;
    }

    public string KeyName { get; }
    public string ApiKey { get; }
}