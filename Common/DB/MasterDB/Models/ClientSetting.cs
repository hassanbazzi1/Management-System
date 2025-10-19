namespace Common.DB.MasterDB.Models;

public class ClientSetting
{
    public int Id { get; set; }

    public int Sid { get; set; }

    public int Cid { get; set; }

    public string Value { get; set; } = null!;

    public DateTime CreateDate { get; set; }
    
    public uint Xmin { get; set; } 

    public virtual Setting Setting { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}