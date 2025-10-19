namespace Common.DB.ClientDB.Models;

public class InstructionsSection
{
    public int Id { get; set; }

    public int Iid { get; set; }

    public short Priority { get; set; }

    public int Order { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Text { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual Instructions Instructions { get; set; } = null!;
}