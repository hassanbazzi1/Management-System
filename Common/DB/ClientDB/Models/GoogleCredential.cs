namespace Common.DB.ClientDB.Models;

public class GoogleCredential
{
    public int Id { get; set; }

    public string ServiceAccount { get; set; } = null!;

    public byte[] Privatekey { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string? N8NCredentialId { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual ICollection<ChatReportInstanceGoogleSheets> ChatReportFormatGoogleSheets { get; set; } = new List<ChatReportInstanceGoogleSheets>();
}