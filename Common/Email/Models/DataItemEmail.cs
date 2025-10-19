namespace Common.Email.Models;

[Serializable]
public class DataItemEmail
{
    public List<string> Tos { get; set; }

    public List<string> Ccs { get; set; }

    public List<string> Bccs { get; set; }

    public string Body { get; set; }

    public string Subject { get; set; }

    public List<string> AttachementFilePaths { get; set; }

    public List<string> AttachementFilePaths_RemoveAfter { get; set; }

    public string DomainId { get; set; }

    public string BodyHtml { get; set; }

    public Dictionary<string, string> CIDSForGmail { get; set; }
}