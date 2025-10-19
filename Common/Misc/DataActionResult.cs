namespace Common.Misc;

public class DataActionResult<T> : ActionResult
{
    public T Data { get; set; }
}