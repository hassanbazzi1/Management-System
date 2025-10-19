using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ChatEscalationStatusDbService : DbFilterService<ClientDbContext,ChatEscalationStatus>
{
    public ChatEscalationStatusDbService(ClientDbContext context) : base(context)
    {
    }

    public async Task<List<ChatEscalationStatus>> GetAllAsync()
    {
        return await Context.ChatEscalationStatuses.ToListAsync();
    }

    public async Task<ChatEscalationStatus?> GetByIdAsync(int id)
    {
        return await Context.ChatEscalationStatuses.FindAsync(id);
    }

    public async Task AddAsync(ChatEscalationStatus status)
    {
        await Context.ChatEscalationStatuses.AddAsync(status);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ChatEscalationStatus status)
    {
        Context.ChatEscalationStatuses.Update(status);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.ChatEscalationStatuses.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.ChatEscalationStatuses.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.ChatEscalationStatuses.Update(entity);
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