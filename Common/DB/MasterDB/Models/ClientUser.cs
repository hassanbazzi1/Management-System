using Microsoft.AspNetCore.Identity;

namespace Common.DB.MasterDB.Models;

public class ClientUser
{
    public int Id { get; set; }

    public string IdentityUserId { get; set; }

    public int? Cid { get; set; }

    public string Username { get; set; } = null!;

    public string? FirstName { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public int TypeId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual Client? Client { get; set; }

    public virtual ClientUserType? ClientUserType { get; set; }

    public virtual IdentityUser IdentityUser { get; set; }

    //public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}