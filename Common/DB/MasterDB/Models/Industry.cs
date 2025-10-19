namespace Common.DB.MasterDB.Models;

public class Industry
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}