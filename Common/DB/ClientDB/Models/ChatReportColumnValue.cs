namespace Common.DB.ClientDB.Models;

public class ChatReportColumnValue
{
    public int Id { get; set; }

    public int ColumnId { get; set; }

    public int ReportId { get; set; }

    public byte[] Value { get; set; }

    public DateTime CreateDate { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ChatReportTemplateColumn ChatReportTemplateColumn { get; set; } = null!;

    public virtual ChatReport ChatReport { get; set; } = null!;
}