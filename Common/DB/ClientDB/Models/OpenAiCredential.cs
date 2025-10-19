namespace Common.DB.ClientDB.Models;

public class OpenAiCredential
{
    public int Id { get; set; }

    public byte[] Token { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string ModelId { get; set; } = null!;

    public string? N8NCredentialId { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 
}