namespace Common.DB.ClientDB.Models;

public class InstructionsRule
{
    public int Id { get; set; }

    public int Iid { get; set; }

    public int Rid { get; set; }

    public short Priority { get; set; }

    public DateTime CreateDate { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual Instructions Instructions { get; set; } = null!;

    public virtual Rule Rule { get; set; } = null!;
}