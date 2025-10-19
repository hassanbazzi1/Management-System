namespace Common.API.ApiMapping.ChatReports.Models;

public class ChatReportResponse
{
    public int Id { get; set; }

    public int InstanceId { get; set; }

    public int Cid { get; set; }

    //public byte[] Data { get; set; }

    public string Name { get; set; }

    public DateTime CreateDate { get; set; }
}