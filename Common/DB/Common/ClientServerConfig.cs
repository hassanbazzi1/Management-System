using Common.DB.MasterDB.Models;

namespace Common.DB.Common;

public class ClientServerConfig
{
    public Server? Server { get; set; }

    public DbCredentials? Credentials { get; set; }
}