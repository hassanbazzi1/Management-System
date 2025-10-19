using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class AIAgentDbService : DbFilterService<ClientDbContext,AiAgent>
{
    public AIAgentDbService(ClientDbContext context) : base(context)
    {
    }

    public async Task<List<AiAgent>> GetAllAsync()
    {
        return await Context.AiAgents.ToListAsync();
    }

    public async Task<AiAgent?> GetByIdAsync(int id)
    {
        return await Context.AiAgents.FindAsync(id);
    }

    public async Task AddAsync(AiAgent agent)
    {
        await Context.AiAgents.AddAsync(agent);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AiAgent agent)
    {
        Context.AiAgents.Update(agent);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.AiAgents.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.AiAgents.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.AiAgents.Update(entity);
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