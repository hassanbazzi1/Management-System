namespace Common.DB.MasterDB.Models;

public class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Services { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public int? IndustryId { get; set; }

    public string? FacebookUrl { get; set; }

    public string? TiktokUrl { get; set; }

    public string? Xurl { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? IdealCustomer { get; set; }

    public bool IsActive { get; set; }

    public bool HasDemo { get; set; }

    public int? ServerId { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<ClientUser> ClientUsers { get; set; } = new List<ClientUser>();

    public virtual ICollection<ClientSetting> ClientSettings { get; set; } = new List<ClientSetting>();

    public virtual Server? Server { get; set; }

    // TODO: Main industry?
    public virtual Industry? Industry { get; set; }
}