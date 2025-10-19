namespace Common.DB.ClientDB.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Services { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string? CompanyName { get; set; } = null!;

    public int IndustryId { get; set; }

    public string? FacebookUrl { get; set; }

    public string? TiktokUrl { get; set; }

    public string? Xurl { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? IdealCustomer { get; set; }

    public bool IsActive { get; set; }

    public bool IsDemo { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual Industry Industry { get; set; } = null!;

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}