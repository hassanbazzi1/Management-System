using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Factories;

public class PostGreSqlClientContextFactory : ClientDbContextFactory
{
    public override ClientDbContext CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ClientDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new ClientDbContext(optionsBuilder.Options);
    }

    public override ClientDbContext CreateDbContext(DbCredentials? credentials)
    {
        var connextionString = "Host=" + credentials.Host + ";" + "Port=" + credentials.Port + ";" + "Database=" + credentials.DbName + ";" + "Username=" + credentials.Username + ";" + "Password=" + credentials.Password + ";";

        return CreateDbContext(connextionString);
    }
}