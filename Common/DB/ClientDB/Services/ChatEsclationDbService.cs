using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ChatEscalationDbService : DbFilterService<ClientDbContext,ChatEscalation>
{
    public ChatEscalationDbService(ClientDbContext context) : base(context)
    {
    }

    public async Task<List<ChatEscalation>> GetAllAsync()
    {
        return await Context.ChatEscalations.ToListAsync();
    }
    public async Task<int> GetEscalationsCount()
    {

       var result = await Context.ChatEscalations.CountAsync();
        return result;
    }
    public async Task<ChatEscalation?> GetByIdAsync(int id)
    {
        return await Context.ChatEscalations.FindAsync(id);
    }

    public async Task AddAsync(ChatEscalation status)
    {
        await Context.ChatEscalations.AddAsync(status);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ChatEscalation status)
    {
        Context.ChatEscalations.Update(status);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.ChatEscalations.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.ChatEscalations.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.ChatEscalations.Update(entity);
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