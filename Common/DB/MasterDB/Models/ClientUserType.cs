namespace Common.DB.MasterDB.Models;

public class ClientUserType
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<ClientUser> ClientUsers { get; set; } = new List<ClientUser>();
}