namespace Common.DB.Common;

public class DbCredentials
{
    public DbCredentials()
    {
    }

    public DbCredentials(string host, int port, string dbName, string username, string password)
    {
        Host = host;
        Port = port;
        DbName = dbName;
        Username = username;
        Password = password;
    }

    public string Host { get; set; }
    public int Port { get; set; }
    public string DbName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}