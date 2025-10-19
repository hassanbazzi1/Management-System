namespace Common.DB.ClientDB.Models;

public class ChatReportInstanceGoogleSheets
{
    public int Id { get; set; }

    public int CredentialId { get; set; }

    public string SheetName { get; set; } = null!;

    public string Url { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreateDate { get; set; }
    
    public uint Xmin { get; set; } 
    public virtual GoogleCredential GoogleCredential { get; set; } = null!;

    public virtual ICollection<ChatReportInstance> ChatReportInstances { get; set; } = new List<ChatReportInstance>();
}