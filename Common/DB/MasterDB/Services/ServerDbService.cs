using Common.DB.Common;
using Common.DB.MasterDB.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.MasterDB.Services;

public class ServerDbService : DbService<MasterDbContext>
{
    public ServerDbService(MasterDbContext context) : base(context)
    {
    }

    // Servers
    public async Task<List<Server>> GetAllServersAsync()
    {
        return await Context.Servers.ToListAsync();
    }

    public async Task<Server?> GetServerByIdAsync(int id)
    {
        return await Context.Servers.FindAsync(id);
    }

    public async Task AddServerAsync(Server server)
    {
        await Context.Servers.AddAsync(server);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateServerAsync(Server server)
    {
        Context.Servers.Update(server);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteServerAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Servers.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Servers.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Servers.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }
}