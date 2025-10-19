namespace Common.DB.ClientDB.Models;

public class SubscriptionType
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreateDate { get; set; }

    public uint Xmin { get; set; } 
    
    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}