namespace Common.API.Models;

public class CommonApiResponse
{
    public enum ApiStatusCode
    {
        Success,
        Failure
    }

    protected static readonly Dictionary<ApiStatusCode, string> ApiStatus = new()
    {
        { ApiStatusCode.Success, "success" },
        { ApiStatusCode.Failure, "failure" }
    };

    public string Status { get; set; }

    public string Message { get; set; }

    public static string GetStatusStatus()
    {
        return ApiStatus[ApiStatusCode.Success];
    }

    public static string GetStatusFailure()
    {
        return ApiStatus[ApiStatusCode.Failure];
    }

    public bool IsStatusSuccess()
    {
        return Status == GetStatusStatus();
    }

    public void SetStatusSuccess()
    {
        Status = GetStatusStatus();
    }

    public void SetStatusFailure()
    {
        Status = GetStatusFailure();
    }

    public void SetStatusFailure(string message)
    {
        Status = GetStatusFailure();
        Message = message;
    }
}