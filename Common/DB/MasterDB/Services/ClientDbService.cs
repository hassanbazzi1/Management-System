using Common.DB.Common;
using Common.DB.MasterDB.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.MasterDB.Services;

public class ClientDbService : DbService<MasterDbContext>
{
    public ClientDbService(MasterDbContext context) : base(context)
    {
    }

    public async Task<List<Client>> GetAllAsync()
    {
        return await Context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await Context.Clients.FindAsync(id);
    }

    public async Task AddAsync(Client client)
    {
        await Context.Clients.AddAsync(client);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        Context.Clients.Update(client);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Clients.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Clients.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Clients.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }
}