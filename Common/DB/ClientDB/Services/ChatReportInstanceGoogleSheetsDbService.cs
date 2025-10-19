using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ChatReportInstanceGoogleSheetsDbService : DbFilterService<ClientDbContext,ChatReportInstanceGoogleSheets>
{
    public ChatReportInstanceGoogleSheetsDbService(ClientDbContext context) : base(context)
    {
    }

    public async Task<List<ChatReportInstanceGoogleSheets>> GetAllGoogleSheetsChatReportInstancesAsync()
    {
        return await Context.ChatReportInstancesGoogleSheets.ToListAsync();
    }

    public async Task<ChatReportInstanceGoogleSheets?> GetGoogleSheetsChatReportInstanceByIdAsync(int id)
    {
        return await Context.ChatReportInstancesGoogleSheets.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddGoogleSheetsChatReportChatReportInstanceAsync(ChatReportInstanceGoogleSheets instance)
    {
        await Context.ChatReportInstancesGoogleSheets.AddAsync(instance);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateGoogleSheetsChatReportInstanceAsync(ChatReportInstanceGoogleSheets instance)
    {
        Context.ChatReportInstancesGoogleSheets.Update(instance);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteGoogleSheetsChatReportInstanceAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.ChatReportInstancesGoogleSheets.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.ChatReportInstancesGoogleSheets.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.ChatReportInstancesGoogleSheets.Update(entity);
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