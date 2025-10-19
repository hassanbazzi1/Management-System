using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ChatReportTemplateDbService : DbFilterService<ClientDbContext,ChatReportTemplate>
{
    public ChatReportTemplateDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<ChatReportTemplate>> GetAllChatReportTemplatesAsync()
    {
        return await Context.ChatReportTemplates.ToListAsync();
    }

    public async Task<ChatReportTemplate?> GetChatReportTemplateByIdAsync(int id)
    {
        return await Context.ChatReportTemplates.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddChatReportTemplateAsync(ChatReportTemplate template)
    {
        await Context.ChatReportTemplates.AddAsync(template);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateChatReportTemplateAsync(ChatReportTemplate template)
    {
        Context.ChatReportTemplates.Update(template);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteChatReportTemplateAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.ChatReportTemplates.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.ChatReportTemplates.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.ChatReportTemplates.Update(entity);
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