using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ChatReportInstanceDbService : DbFilterService<ClientDbContext,ChatReportInstance>
{
    public ChatReportInstanceDbService(ClientDbContext context) : base(context)
    {
    }

    public async Task<List<ChatReportInstance>> GetAllChatReportInstancesAsync()
    {
        return await Context.ChatReportInstances.ToListAsync();
    }

    public async Task<ChatReportInstance?> GetChatReportInstanceByIdAsync(int id)
    {
        return await Context.ChatReportInstances.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<ChatReportInstance>> GetChatReportInstancesByTemplateIdAsync(int id)
    {
        return await Context.ChatReportInstances.Where(c => c.TemplateId == id).ToListAsync();
    }

    public async Task AddChatReportChatReportInstanceAsync(ChatReportInstance instance)
    {
        await Context.ChatReportInstances.AddAsync(instance);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateChatReportInstanceAsync(ChatReportInstance instance)
    {
        Context.ChatReportInstances.Update(instance);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteChatReportInstanceAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.ChatReportInstances.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.ChatReportInstances.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.ChatReportInstances.Update(entity);
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