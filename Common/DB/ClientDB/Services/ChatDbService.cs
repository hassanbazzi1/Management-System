using Common.API.Models;
using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ChatDbService : DbFilterService<ClientDbContext,Chat>
{
    public ChatDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<Chat>> GetAllChatsAsync(DataQueryOptions? filter,bool includeChatMessages)
    {
        IQueryable<Chat> baseData;
        baseData = filter == null ? Context.Chats : QueryableDataService.ApplyDataQuery(filter,Context.Chats,GetDefaultSorting(Context));
        
        return await (includeChatMessages ? baseData.Include(c => c.ChatMessages).ToListAsync() : baseData.ToListAsync());
        
    }
    
    public async Task<int> GetChatsCount()
    {
        var result = await Context.Chats.CountAsync();


        return result;
    }

    
    public async Task<Chat?> GetChatByIdAsync(int id)
    {
        return await Context.Chats.Include(c => c.ChatMessages).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddChatAsync(Chat chat)
    {
        await Context.Chats.AddAsync(chat);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateChatAsync(Chat chat)
    {
        Context.Chats.Update(chat);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteChatAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Chats.FindAsync(id);

        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Chats.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Chats.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }
    public static string? GetDefaultSorting(ClientDbContext context)
    {
        return nameof(Chat.StartDate);
    }

    public static List<string> GetDefaultSearchColumns(ClientDbContext context)
    {
        throw new NotImplementedException();
    }
}