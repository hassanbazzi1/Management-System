namespace Common.DB.ClientDB.Models;

public class Subscription
{
    public int Id { get; set; }

    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual SubscriptionType SubscriptionType { get; set; } = null!;

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}