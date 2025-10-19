namespace Common.DB.ClientDB.Models;

public class ChatMessage
{
    public int Id { get; set; }

    public int Cid { get; set; }

    public DateTime Date { get; set; }

    public bool IsUser { get; set; }

    public bool IsDeleted { get; set; }

    public string Text { get; set; } = null!;
    
    public uint Xmin { get; set; } 

    public virtual Chat Chat { get; set; } = null!;
}