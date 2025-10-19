using Common.API.Models;
using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ChatMessageDbService : DbFilterService<ClientDbContext,ChatMessage>
{
    public ChatMessageDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<ChatMessage>> GetMessagesByChatIdAsync(long chatId)
    {
        return await Context.ChatMessages.Where(m => m.Cid == chatId).ToListAsync();
    }

    public async Task<ChatMessage?> GetMessageByIdAsync(int id)
    {
        return await Context.ChatMessages.FindAsync(id);
    }

    public async Task AddMessageAsync(ChatMessage message)
    {
        await Context.ChatMessages.AddAsync(message);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateMessageAsync(ChatMessage message)
    {
        Context.ChatMessages.Update(message);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteMessageAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.ChatMessages.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.ChatMessages.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.ChatMessages.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }

    public static string GetDefaultSorting(ClientDbContext context)
    {
        return nameof(ChatMessage.Date);
    }
    
    public static List<string> GetDefaultSearchColumns(ClientDbContext context)
    {
        throw new NotImplementedException();
    }
}