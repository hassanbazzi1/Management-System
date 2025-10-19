namespace Common.DB.ClientDB.Models;

public class ChatReport
{
    public int Id { get; set; }

    public int InstanceId { get; set; }

    public int Cid { get; set; }

    public byte[] Data { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreateDate { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual Chat Chat { get; set; } = null!;

    public virtual ChatReportInstance ChatReportInstance { get; set; } = null!;

    public virtual ICollection<ChatReportColumnValue> ChatReportColumnValues { get; set; } = new List<ChatReportColumnValue>();
}