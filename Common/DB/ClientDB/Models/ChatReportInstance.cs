namespace Common.DB.ClientDB.Models;

public class ChatReportInstance
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int TemplateId { get; set; }

    public int GoogleSheetsId { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ChatReportTemplate ChatReportTemplate { get; set; } = null!;

    public virtual ChatReportInstanceGoogleSheets? ChatReportInstanceGoogleSheets { get; set; }

    public virtual ICollection<ChatReport> ChatReports { get; set; } = new List<ChatReport>();
}