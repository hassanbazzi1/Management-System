namespace Common.Misc;

public class ActionResult
{
    public enum ActionStatus
    {
        Success,
        Failure
    }

    public ActionStatus Status { get; set; }
    public string Message { get; set; }

    public List<ActionResult>? ChildResult { get; set; }
    public Exception Exception { get; set; }

    public bool IsSuccess()
    {
        return Status == ActionStatus.Success;
    }

    public void AddChildResult(ActionResult result)
    {
        if (ChildResult == null)
            ChildResult = new List<ActionResult>();

        ChildResult.Add(result);
    }
}