namespace Common.DB.MasterDB.Models;

public class AiAgentMessageTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Text { get; set; } = null!;

    public DateTime CreateDate { get; set; }
    
    public uint Xmin { get; set; } 
}