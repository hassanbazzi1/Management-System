using Common.DB.Common;

namespace Common.DB.ClientDB.Factories;

public abstract class ClientDbContextFactory
{
    public abstract ClientDbContext CreateDbContext(string connectionString);

    public abstract ClientDbContext CreateDbContext(DbCredentials? credentials);
}