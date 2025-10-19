namespace Common.DB.ClientDB.Models;

public class ChatReportTemplateColumnType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<ChatReportTemplateColumn> ChatReportTemplateColumns { get; set; } = new List<ChatReportTemplateColumn>();
}