using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class HumanAgentDbService : DbFilterService<ClientDbContext,HumanAgent>
{
    public HumanAgentDbService(ClientDbContext context) : base(context)
    {
    }

    public async Task<List<HumanAgent>> GetAllAsync()
    {
        return await Context.HumanAgents.ToListAsync();
    }

    public async Task<HumanAgent?> GetByIdAsync(int id)
    {
        return await Context.HumanAgents.FindAsync(id);
    }

    public async Task AddAsync(HumanAgent agent)
    {
        await Context.HumanAgents.AddAsync(agent);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(HumanAgent agent)
    {
        Context.HumanAgents.Update(agent);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.HumanAgents.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.HumanAgents.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.HumanAgents.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }

    public static string GetDefaultSorting(ClientDbContext context)
    {
        throw new NotImplementedException();
    }

    public static List<string> GetDefaultSearchColumns(ClientDbContext context)
    {
        throw new NotImplementedException();
    }
}