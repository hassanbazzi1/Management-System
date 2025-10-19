namespace Common.DB.ClientDB.Models;

public class ChatReportTemplateColumn
{
    public int Id { get; set; }

    public int TemplateId { get; set; }

    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ChatReportTemplate ChatReportTemplate { get; set; } = null!;

    public virtual ChatReportTemplateColumnType ChatReportTemplateColumnType { get; set; } = null!;

    public virtual ICollection<ChatReportColumnValue> ChatReportColumnValues { get; set; } = new List<ChatReportColumnValue>();
}