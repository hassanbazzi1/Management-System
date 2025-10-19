namespace Common.DB.ClientDB.Models;

public class Document
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public float[] Embeddings { get; set; }
    
    public uint Xmin { get; set; } 
}