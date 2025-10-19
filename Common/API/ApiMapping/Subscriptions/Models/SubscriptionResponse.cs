namespace Common.API.ApiMapping.Subscriptions.Models;

public class SubscriptionResponse
{
    public int Id { get; set; }

    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreateDate { get; set; }
}