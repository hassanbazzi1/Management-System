namespace Common.DB.ClientDB.Models;

public class ChatReportTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int FormatId { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public ChatReportFormat Format { get; set; } = null!;

    public virtual ICollection<ChatReportInstance> ChatReportInstances { get; set; } = new List<ChatReportInstance>();

    public virtual ICollection<ChatReportTemplateColumn> ChatReportTemplateColumns { get; set; } = new List<ChatReportTemplateColumn>();
}