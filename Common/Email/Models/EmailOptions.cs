namespace Common.Email.Models;

public class EmailOptions
{
    // Not needed in current implementations
    /*
    public string Host { get; set; }

    public int Port { get; set; }

    public bool UseSsl { get; set; }
    
    public string Password { get; set; }
    */
    
    public string SenderEmail { get; set; }
    public string Username { get; set; }
    
}