using Common.DB.ClientDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Industry = Common.DB.ClientDB.Models.Industry;

namespace Common.DB.ClientDB;

public partial class ClientDbContext : DbContext
{
    // TODO: Add required() call to foreign keys that need it
    public ClientDbContext()
    {
    }

    public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
    {
    }

    // For Migrations Only

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Username=postgres;Password=postgres");
        }
    }
    // TODO: Add seeding data?

    public virtual DbSet<AiAgent> AiAgents { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }
    
    public virtual DbSet<ChatEscalation> ChatEscalations { get; set; }
    
    public virtual DbSet<ChatEscalationStatus> ChatEscalationStatuses { get; set; }
    
    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<ChatReportTemplate> ChatReportTemplates { get; set; }

    public virtual DbSet<ChatReportFormat> ChatReportFormats { get; set; }

    public virtual DbSet<ChatReportInstance> ChatReportInstances { get; set; }

    public virtual DbSet<ChatReportInstanceGoogleSheets> ChatReportInstancesGoogleSheets { get; set; }

    public virtual DbSet<ChatReportTemplateColumn> ChatReportTemplateColumns { get; set; }

    public virtual DbSet<ChatReportTemplateColumnType> ChatReportTemplateColumnTypes { get; set; }

    public virtual DbSet<ChatReportColumnValue> ChatReportColumnValues { get; set; }
    public virtual DbSet<ChatReport> ChatReports { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<HumanAgent> HumanAgents { get; set; }

    public virtual DbSet<Instructions> Instructions { get; set; }

    public virtual DbSet<InstructionsSection> InstructionsSections { get; set; }
    public virtual DbSet<InstructionsRule> InstructionsRules { get; set; }

    public virtual DbSet<OpenAiCredential> OpenaiCredentials { get; set; }

    public virtual DbSet<GoogleCredential> GoogleCredentials { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }

    public virtual DbSet<RuleSet> RuleSets { get; set; }

    public virtual DbSet<RuleSetRule> RuleSetRules { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<TwilioCredential> TwilioCredentials { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    public virtual DbSet<WorkflowTemplate> WorkflowTemplates { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasPostgresExtension("vector");
        modelBuilder.HasDefaultSchema("client_template");

        modelBuilder.Entity<AiAgent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ai_agent_pk");

            entity.ToTable("ai_agent", "client_template");

            entity.HasIndex(e => e.Code, "ai_agent_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20).HasColumnName("code").IsRequired();
            entity.Property(e => e.Wid).HasColumnName("wid").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.InstructionsId).HasColumnName("instructions_id").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Instructions).WithMany(p => p.AiAgents).HasForeignKey(d => d.InstructionsId).HasConstraintName("ai_agent_instructions_id_fk");

            entity.HasOne(d => d.Workflow).WithMany(p => p.AiAgents).HasForeignKey(d => d.Wid).HasConstraintName("ai_agent_workflow__fk");
        });

        modelBuilder.Entity<HumanAgent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("human_agent_pk");

            entity.ToTable("human_agent", "client_template");

            entity.HasIndex(e => e.Phone, "human_agent_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(50).HasColumnName("first_name").IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(50).HasColumnName("last_name").IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(50).HasColumnName("phone").IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_pk");

            entity.ToTable("chat", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.StartDate).HasColumnType("timestamp with time zone").HasColumnName("start_date").IsRequired();
            entity.Property(e => e.EndDate).HasColumnType("timestamp with time zone").HasColumnName("end_date");
            entity.Property(e => e.ConversationId).HasColumnName("conversation_id").IsRequired();
            entity.Property(e => e.EscalationId).HasColumnName("escalation_id");

            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
            
            entity.HasOne(d => d.Conversation).WithMany(p => p.Chats).HasForeignKey(d => d.ConversationId).HasConstraintName("chat_conversation_fk");
            entity.HasOne(e => e.ChatEscalation).WithOne(e => e.Chat).HasForeignKey<ChatEscalation>(e => e.SourceChatId).HasPrincipalKey<Chat>(c=>c.Id).HasConstraintName("chat_chat_escalation_fk");
        });
        
        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("conversation_pk");

            entity.ToTable("conversation", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(e => e.AiAgentId).HasColumnName("ai_agent_id");
            entity.Property(e => e.HumanAgentId).HasColumnName("human_agent_id");
            entity.Property(e => e.LastChatMessageId).HasColumnName("last_chat_message_id");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
            
            entity.HasOne(d => d.User).WithMany(p => p.Conversations).HasForeignKey(d => d.UserId).HasConstraintName("conversation_user_fk");
            entity.HasOne(d => d.HumanAgent).WithMany(p => p.Conversations).HasForeignKey(d => d.HumanAgentId).HasConstraintName("conversation_human_agent_fk");
            entity.HasOne(d => d.AiAgent).WithMany(p => p.Conversations).HasForeignKey(d => d.AiAgentId).HasConstraintName("conversation_ai_agent_fk");
            entity.HasOne(e => e.LastChatMessage).WithOne().HasForeignKey<Conversation>(e => e.LastChatMessageId).HasConstraintName("chat_conversation_last_chat_message_fk");
        });
        
        modelBuilder.Entity<ChatEscalation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_escalation_pk");

            entity.ToTable("chat_escalation", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.SourceChatId).HasColumnName("source_chat_id").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.StatusId).HasColumnName("status_id").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            
            entity.HasOne(d=>d.ChatEscalationStatus).WithMany(x=>x.ChatEscalations).HasForeignKey(d => d.StatusId).HasConstraintName("chat_escalation_status_fk");
        });
        
        modelBuilder.Entity<ChatEscalationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_escalation_status_pk");

            entity.ToTable("chat_escalation_status", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20).HasColumnName("code").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description"); 
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_message_pk");

            entity.ToTable("chat_message", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Cid).HasColumnName("cid").IsRequired();
            entity.Property(e => e.Date).HasColumnType("timestamp with time zone").HasColumnName("date").IsRequired();
            entity.Property(e => e.IsUser).HasColumnName("is_user").IsRequired();
            entity.Property(e => e.Text).IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatMessages).HasForeignKey(d => d.Cid).HasConstraintName("chat_message_chat_fk");
        });

        modelBuilder.Entity<ChatReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_report_pk");

            entity.ToTable("chat_report", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.InstanceId).HasColumnName("instance_id").IsRequired();
            entity.Property(e => e.Cid).HasColumnName("cid").IsRequired();
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.ChatReportInstance).WithMany(p => p.ChatReports).HasForeignKey(d => d.InstanceId).HasConstraintName("chat_report_instance_fk");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatReports).HasForeignKey(d => d.Cid).HasConstraintName("chat_report_chat_fk");
        });

        modelBuilder.Entity<ChatReportColumnValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_report_column_value_pk");

            entity.ToTable("chat_report_column_value", "client_template");

            entity.Property(e => e.ColumnId).HasColumnName("column_id").IsRequired();
            entity.Property(e => e.Value).HasColumnName("value").IsRequired();
            entity.Property(e => e.ReportId).HasColumnName("report_id").IsRequired();
            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.ChatReport).WithMany(p => p.ChatReportColumnValues).HasForeignKey(d => d.ReportId).HasConstraintName("chat_report_column_value_report_fk");

            entity.HasOne(d => d.ChatReportTemplateColumn).WithMany(p => p.ChatReportColumnValues).HasForeignKey(d => d.ColumnId).HasConstraintName("chat_report_column_value_column_fk");
        });

        modelBuilder.Entity<ChatReportFormat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_report_format_pk");
            entity.HasIndex(e => e.Code, "chat_report_format_unique_key").IsUnique();

            entity.ToTable("chat_report_format", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20).HasColumnName("code").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
    
        });

        modelBuilder.Entity<ChatReportInstance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_report_instance_pk");

            entity.ToTable("chat_report_instance", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.TemplateId).HasColumnName("template_id").IsRequired();
            entity.Property(e => e.GoogleSheetsId).HasColumnName("google_sheets_id");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.ChatReportTemplate).WithMany(p => p.ChatReportInstances).HasForeignKey(d => d.TemplateId).HasConstraintName("chat_report_instance_template_fk");

            // TODO: Should only be one-to-one?
            entity.HasOne(d => d.ChatReportInstanceGoogleSheets).WithMany(p => p.ChatReportInstances).HasForeignKey(d => d.GoogleSheetsId).HasConstraintName("chat_report_instance_google_sheets_fk");
        });

        modelBuilder.Entity<ChatReportInstanceGoogleSheets>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_report_instance_google_sheets_pk");

            entity.ToTable("chat_report_instance_google_sheets", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.CredentialId).HasColumnName("credential_id").IsRequired();
            entity.Property(e => e.SheetName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Url).HasColumnName("url").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.GoogleCredential).WithMany(p => p.ChatReportFormatGoogleSheets).HasForeignKey(d => d.CredentialId).HasConstraintName("chat_report_format_google_sheets_credential_fk");
        });

        modelBuilder.Entity<ChatReportTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_report_template_pk");

            entity.ToTable("chat_report_template", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.FormatId).HasColumnName("format_id").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Format).WithMany(p => p.ChatReportTemplates).HasForeignKey(d => d.FormatId).HasConstraintName("chat_report_template_format_fk");
        });

        modelBuilder.Entity<ChatReportTemplateColumn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_report_template_column_pk");

            entity.ToTable("chat_report_template_column", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.TemplateId).HasColumnName("template_id").IsRequired();
            entity.Property(e => e.TypeId).HasColumnName("type_id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.ChatReportTemplate).WithMany(p => p.ChatReportTemplateColumns).HasForeignKey(d => d.TemplateId).HasConstraintName("chat_report_template_column_template_fk");

            entity.HasOne(d => d.ChatReportTemplateColumnType).WithMany(p => p.ChatReportTemplateColumns).HasForeignKey(d => d.TypeId).HasConstraintName("chat_report_template_column_type_fk");
        });

        modelBuilder.Entity<ChatReportTemplateColumnType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_report_template_column_type_pk");

            entity.ToTable("chat_report_template_column_type", "client_template");

            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("documents_pkey");
            entity.ToTable("document", "client_template");
        });

        modelBuilder.Entity<Instructions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("instructions_pk");

            entity.ToTable("instructions", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date");
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

        });

        modelBuilder.Entity<InstructionsSection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("instructions_section_pk");

            entity.ToTable("instructions_section", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Iid).HasColumnName("iid").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.Order).HasColumnName("order").IsRequired();
            entity.Property(e => e.Priority).HasColumnName("priority").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.Text).HasColumnName("text").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Instructions).WithMany(p => p.InstructionsSections).HasForeignKey(d => d.Iid).HasConstraintName("instructions_section_instruction_fk");
        });

        modelBuilder.Entity<InstructionsRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("instructions_rule_pk");

            entity.ToTable("instructions_rule", "client_template");

            entity.HasIndex(e => new { rid = e.Rid, iid = e.Iid }, "instructions_rule_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Iid).HasColumnName("iid").IsRequired();
            entity.Property(e => e.Rid).HasColumnName("rid").IsRequired();
            entity.Property(e => e.Priority).HasColumnName("priority").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Instructions).WithMany(p => p.InstructionsRules).HasForeignKey(d => d.Iid).HasConstraintName("instructions_rule_instruction_fk");

            entity.HasOne(d => d.Rule).WithMany(p => p.InstructionsRules).HasForeignKey(d => d.Rid).HasConstraintName("instructions_rule_rule_fk");
        });

        modelBuilder.Entity<OpenAiCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("openai_credential_pk");

            entity.ToTable("openai_credential", "client_template");

            entity.HasIndex(e => e.Token, "openai_credential_unique_key").IsUnique();
            entity.HasIndex(e => e.Name, "openai_credential_unique_key2").IsUnique();
            entity.HasIndex(e => e.N8NCredentialId, "openai_credential_unique_key3").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Token).HasColumnName("token").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.ModelId).HasMaxLength(50).HasColumnName("model_id").IsRequired();
            entity.Property(e => e.N8NCredentialId).HasMaxLength(500).HasColumnName("n8n_credential_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
    
        });

        modelBuilder.Entity<GoogleCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("google_credential_pk");

            entity.ToTable("google_credential", "client_template");

            entity.HasIndex(e => e.N8NCredentialId, "google_credential_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.ServiceAccount).HasColumnName("service_account").IsRequired();
            entity.Property(e => e.Privatekey).HasColumnName("private_key").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.N8NCredentialId).HasMaxLength(500).HasColumnName("n8n_credential_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
    
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rule_pk");

            entity.ToTable("rule", "client_template");

            entity.Property(e => e.Text).HasColumnName("text").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
    
        });

        modelBuilder.Entity<RuleSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rule_set_pk");

            entity.ToTable("rule_set", "client_template");

            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

        });

        modelBuilder.Entity<RuleSetRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rule_set_rules_pk");

            entity.ToTable("rule_set_rules", "client_template");

            entity.HasIndex(e => new { rid = e.Rid, rsid = e.Rsid }, "rule_set_rules_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Rid).HasColumnName("rid").IsRequired();
            entity.Property(e => e.Rsid).HasColumnName("rsid").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Rule).WithMany(p => p.RuleSetRules).HasForeignKey(d => d.Rid).HasConstraintName("rule_set_rules_rule_fk");

            entity.HasOne(d => d.RuleSet).WithMany(p => p.RuleSetRules).HasForeignKey(d => d.Rsid).HasConstraintName("rule_set_rules_rule_set_fk");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_pk");

            entity.ToTable("session", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.ConversationId).HasColumnName("conversation_id").IsRequired();
            entity.Property(e => e.StartDate).HasColumnType("timestamp with time zone").HasColumnName("start_date").IsRequired();
            entity.Property(e => e.EndDate).HasColumnType("timestamp with time zone").HasColumnName("end_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Conversation).WithMany(p => p.Sessions).HasForeignKey(d => d.ConversationId).HasConstraintName("session_conversation__fk");
            
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user", "client_template");

            entity.HasIndex(e => e.Phone, "user_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(50).HasColumnName("first_name").IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(50).HasColumnName("last_name").IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email").IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(50).HasColumnName("phone").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
    
        });

        modelBuilder.Entity<TwilioCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("twilio_credential_pk");

            entity.ToTable("twilio_credential", "client_template");

            entity.HasIndex(e => e.Token, "twilio_credential_unique_key").IsUnique();
            entity.HasIndex(e => e.Name, "twilio_credential_unique_key2").IsUnique();
            entity.HasIndex(e => e.N8NCredentialId, "twilio_credential_unique_key3").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Token).HasColumnName("token").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.N8NCredentialId).HasMaxLength(500).HasColumnName("n8n_credential_id");
            entity.Property(e => e.Sid).HasMaxLength(500).HasColumnName("sid").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workflow_pk");

            entity.ToTable("workflow", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.Json).HasColumnType("json").HasColumnName("json");
            entity.Property(e => e.TemplateId).HasColumnName("template_id").IsRequired();
            entity.Property(e => e.ProjectId).HasColumnName("project_id").IsRequired();
            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Subscription).WithMany(p => p.Workflows).HasForeignKey(d => d.SubscriptionId).HasConstraintName("workflow_subscription_fk");

            entity.HasOne(d => d.Project).WithMany(p => p.Workflows).HasForeignKey(d => d.ProjectId).HasConstraintName("workflow_project_fk");

            entity.HasOne(d => d.WorkflowTemplate).WithMany(p => p.Workflows).HasForeignKey(d => d.TemplateId).HasConstraintName("workflow_template_fk");
        });

        modelBuilder.Entity<WorkflowTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workflow_template_pk");

            entity.ToTable("workflow_template", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.Json).HasColumnType("json").HasColumnName("json");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
    
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscription_pk");
            entity.ToTable("subscription", "client_template");

            entity.HasIndex(e => e.Name, "subscription_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.TypeId).HasColumnName("type_id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.StartDate).HasColumnType("timestamp with time zone").HasColumnName("start_date").IsRequired();
            entity.Property(e => e.EndDate).HasColumnType("timestamp with time zone").HasColumnName("end_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.SubscriptionType).WithMany(p => p.Subscriptions).HasForeignKey(d => d.TypeId).HasConstraintName("subscription_subscription_type_fk");
        });

        modelBuilder.Entity<SubscriptionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscription_type_pk");
            entity.ToTable("subscription_type", "client_template");

            entity.HasIndex(e => e.Code, "subscription_type_unique_key").IsUnique();

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
            entity.ToTable("industry", "client_template");

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.IsDefault).HasColumnName("is_default").IsRequired();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("project_pk");

            entity.ToTable("project", "client_template");

            entity.HasIndex(e => e.Name, "project_unique_key").IsUnique();

            entity.Property(e => e.Id).UseIdentityColumn().HasIdentityOptions(startValue: 1, incrementBy: 1).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasColumnType("timestamp with time zone").HasColumnName("create_date").IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email").IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(50).HasColumnName("phone").IsRequired();
            entity.Property(e => e.Services).HasColumnName("services");
            entity.Property(e => e.Website).HasMaxLength(100).HasColumnName("website").IsRequired();
            entity.Property(e => e.CompanyName).HasMaxLength(100).HasColumnName("company_name");
            entity.Property(e => e.IndustryId).HasColumnName("industry_id").IsRequired();
            entity.Property(e => e.FacebookUrl).HasMaxLength(500).HasColumnName("facebook_url");
            entity.Property(e => e.TiktokUrl).HasMaxLength(00).HasColumnName("tiktok_url");
            entity.Property(e => e.Xurl).HasMaxLength(500).HasColumnName("x_url");
            entity.Property(e => e.LinkedInUrl).HasMaxLength(500).HasColumnName("linkedin_url");
            entity.Property(e => e.InstagramUrl).HasMaxLength(500).HasColumnName("instagram_url");
            entity.Property(e => e.IdealCustomer).HasMaxLength(500).HasColumnName("ideal_customer");
            entity.Property(e => e.IsActive).HasColumnName("is_active").IsRequired();
            entity.Property(e => e.IsDemo).HasColumnName("is_demo").IsRequired();
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
            entity.Property(p => p.Xmin).IsRowVersion().HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

            entity.HasOne(d => d.Industry).WithMany(p => p.Projects).HasForeignKey(d => d.IndustryId).HasConstraintName("project_industry_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}