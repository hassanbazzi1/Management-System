using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ChatReportDbService : DbFilterService<ClientDbContext,ChatReport>
{
    public ChatReportDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<ChatReport>> GetAllChatReportsAsync()
    {
        return await Context.ChatReports.ToListAsync();
    }

    public async Task<List<ChatReport>> GetChatReportsByChatIdAsync(long cid)
    {
        return await Context.ChatReports.Where(m => m.Cid == cid).ToListAsync();
    }

    public async Task<List<ChatReport>> GetChatReportsByInstanceIdAsync(int instanceId)
    {
        return await Context.ChatReports.Where(m => m.InstanceId == instanceId).ToListAsync();
    }

    public async Task<ChatReport?> GetChatReportByIdAsync(int id)
    {
        return await Context.ChatReports.FindAsync(id);
    }

    public async Task AddChatReportAsync(ChatReport report)
    {
        await Context.ChatReports.AddAsync(report);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateChatReportAsync(ChatReport report)
    {
        Context.ChatReports.Update(report);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteChatReportAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.ChatReports.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.ChatReports.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.ChatReports.Update(entity);
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