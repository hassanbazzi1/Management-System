namespace Common.API.Models;

public class ApiResponse<T> : CommonApiResponse
{
    // TODO: Add other fields
    public T Data { get; set; }

    public void SetStatusSuccess(T data)
    {
        SetStatusSuccess();
        Data = data;
    }
}