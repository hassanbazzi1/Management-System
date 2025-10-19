namespace Common.DB.ClientDB.Models;

public class TwilioCredential
{
    public int Id { get; set; }

    public byte[] Token { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string? N8NCredentialId { get; set; }

    public string Sid { get; set; } = null!;

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 
}