using Common.DB.MasterDB.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DB.MasterDB;

public partial class MasterDbContext : IdentityDbContext
{
    public MasterDbContext()
    {
    }

    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
        
    }
    
    // For Migrations Only
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Username=postgres;Password=postgres");
    }
    
    
    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientSetting> ClientSettings { get; set; }

    public virtual DbSet<ClientUser> ClientUsers { get; set; }

    public virtual DbSet<ClientUserType> ClientUserTypes { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<AiAgentMessageTemplate> MessageTemplates { get; set; }

    public virtual DbSet<AiAgent> AiAgents { get; set; }

    public virtual DbSet<AiAgentInstructions> AiAgentInstructions { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    // Using .NET Identity Instead
    /*
    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }
    */


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("master");
        modelBuilder.UseIdentityAlwaysColumns();
        
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pk");

            entity.ToTable("client", "master");
            
            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email").IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(50).HasColumnName("phone").IsRequired();
            entity.Property(e => e.Services).HasColumnName("services").IsRequired();
            entity.Property(e => e.Website).HasMaxLength(100).HasColumnName("website").IsRequired();
            entity.Property(e => e.CompanyName).HasMaxLength(100).HasColumnName("company_name").IsRequired();
            entity.Property(e => e.IndustryId).HasColumnName("industry_id");
            entity.Property(e => e.FacebookUrl).HasMaxLength(500).HasColumnName("facebook_url");
            entity.Property(e => e.TiktokUrl).HasMaxLength(00).HasColumnName("tiktok_url");
            entity.Property(e => e.Xurl).HasMaxLength(500).HasColumnName("x_url");
            entity.Property(e => e.LinkedInUrl).HasMaxLength(500).HasColumnName("linkedin_url");
            entity.Property(e => e.InstagramUrl).HasMaxLength(500).HasColumnName("instagram_url");
            entity.Property(e => e.IdealCustomer).HasMaxLength(500).HasColumnName("ideal_customer");
            entity.Property(e => e.IsActive).HasColumnName("is_active").IsRequired();
            entity.Property(e => e.HasDemo).HasColumnName("has_demo").IsRequired();
            entity.Property(e => e.ServerId).HasColumnName("server_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
            
            entity.HasOne(d => d.Industry).WithMany(p => p.Clients).HasForeignKey(d => d.IndustryId).HasConstraintName("client_industry_fk");
            entity.HasOne(d => d.Server).WithMany(p => p.Clients).HasForeignKey(d => d.ServerId).HasConstraintName("client_server_fk");
        });

        modelBuilder.Entity<ClientSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_setting_pk");

            entity.ToTable("client_setting", "master");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Sid).HasColumnName("sid").IsRequired();
            entity.Property(e => e.Cid).HasColumnName("cid").IsRequired();
            entity.Property(e => e.Value).HasColumnName("value").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Setting).WithMany(p => p.ClientSettings).HasForeignKey(d => d.Sid).HasConstraintName("client_setting_setting_id_fk");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientSettings).HasForeignKey(d => d.Cid).HasConstraintName("client_setting_client_fk");
        });

        modelBuilder.Entity<ClientUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_user_pk");
            entity.ToTable("client_user", "master");

            entity.HasIndex(e => new { cid = e.Cid, username = e.Username }, "client_user_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Cid).HasColumnName("cid").IsRequired();
            entity.Property(e => e.Username).HasMaxLength(50).HasColumnName("username").IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(50).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasMaxLength(50).HasColumnName("last_name");
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email").IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(50).HasColumnName("phone").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.TypeId).HasColumnName("type_id").IsRequired();
            entity.Property(e => e.IsActive).HasColumnName("is_active").IsRequired();
            entity.Property(e => e.IdentityUserId).HasColumnName("identity_user_id").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            // Add foreign key to IdentityUser
            entity.HasOne(d => d.IdentityUser).WithOne().HasForeignKey<ClientUser>(d => d.IdentityUserId).HasConstraintName("client_user_identity_user_fk").IsRequired();

            entity.HasOne(d => d.Client).WithMany(p => p.ClientUsers).HasForeignKey(d => d.Cid).HasConstraintName("client_user_client_id_fk");

            entity.HasOne(d => d.ClientUserType).WithMany(p => p.ClientUsers).HasForeignKey(d => d.TypeId).HasConstraintName("client_user_type_fk");
        });

        modelBuilder.Entity<ClientUserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_user_type_pk");
            entity.ToTable("client_user_type", "master");

            entity.HasIndex(e => e.Code, "client_user_type_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20).HasColumnName("code").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("setting_pk");
            entity.ToTable("setting", "master");

            entity.HasIndex(e => e.Code, "setting_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20).HasColumnName("code").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("industry_pk");
            entity.ToTable("industry", "master");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
        });

        modelBuilder.Entity<AiAgentMessageTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ai_agent_message_template_pk");
            entity.ToTable("ai_agent_message_template", "master");

            entity.HasIndex(e => e.Name, "message_template_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(500).HasColumnName("name").IsRequired();
            entity.Property(e => e.Text).HasColumnName("text").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
        });

        modelBuilder.Entity<AiAgent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ai_agent_pk");

            entity.ToTable("ai_agent", "master");

            entity.HasIndex(e => e.Code, "ai_agent_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20).HasColumnName("code").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.InstructionsId).HasColumnName("instructions_id").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.AiAgentInstructions).WithMany(p => p.AiAgents).HasForeignKey(d => d.InstructionsId).HasConstraintName("ai_agent_instructions_fk");
        });

        modelBuilder.Entity<AiAgentInstructions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ai_agent_instructions_pk");

            entity.ToTable("ai_agent_instructions", "master");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Text).HasColumnName("text").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
        });

        // Using .NET Identity Instead
        /*
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permission_pk");

            entity.ToTable("permission", "master");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pk");

            entity.ToTable("role", "master");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.IsDefault).HasColumnName("is_default");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_role_pk");

            entity.ToTable("user_role", "master");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.Rid).HasColumnName("rid");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.Uid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_client_user_id_fk");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.Rid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_role_id_fk");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_permission_pk");

            entity.ToTable("role_permission", "master");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id");
            entity.Property(e => e.Rid).HasColumnName("rid");
            entity.Property(e => e.Pid).HasColumnName("uid");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.Rid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_permission_id_fk");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.Pid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_role_id_fk");
        });
        */

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("server_pk");

            entity.ToTable("server", "master");

            entity.HasIndex(e => e.Name, "server_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(200).HasColumnName("name").IsRequired();
            entity.Property(e => e.Url).HasColumnName("url").IsRequired();
            entity.Property(e => e.IsClient).HasColumnName("is_client").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}