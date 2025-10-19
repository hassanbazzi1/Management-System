namespace Common.DB.MasterDB.Models;

public class Server
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public bool IsClient { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}