using Common.API.ApiMapping.Conversations.Mappers;
using Common.API.ApiMapping.Conversations.Models;
using Common.API.Models;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Common.DB.ClientDB.Services;

public class ConversationDbService : DbFilterService<ClientDbContext,Conversation>
{
    public ConversationDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<Conversation>> GetAllConversationsAsync(DataQueryOptions? filter)
    {
        IQueryable<Conversation> baseData;
        baseData = filter == null ? Context.Conversations : QueryableDataService.ApplyDataQuery(filter,Context.Conversations,GetDefaultSorting(Context));
        var data = await baseData.Where(x => x.AiAgentId != null && !x.IsDeleted).Include(x=>x.User).Include(x=>x.LastChatMessage).ToListAsync();
        return data;
    }

    public async Task<List<ConversationChats>> GetChatsForConversation(DataQueryOptions? filter,int conversationId)
    {
        IQueryable<ConversationChats> baseData;
        var chatFilterService = new QueryableDataService<ClientDbContext,ChatMessage>(Context);
        // TODO: Is it better to use Chat and include chat messages for each chat
        if (filter == null)
        {
            //baseData = Context.ChatMessages.Include(x => x.Chat).Where(x => !x.IsDeleted && !x.Chat.IsDeleted && x.Chat.ConversationId == conversationId).GroupBy(x => x.Chat).Select(g => new ConversationChats { Chat = g.Key, ChatMessages = g.ToList() });
            baseData = Context.Chats.Include(x => x.ChatMessages).Where(x => !x.IsDeleted && x.ConversationId == conversationId).Select(g=>new ConversationChats { Chat = g, ChatMessages = g.ChatMessages.ToList() });

        }
        else
        {
            var data = chatFilterService.ApplyDataQuery(filter, Context.ChatMessages.Include(x => x.Chat).Where(x => !x.Chat.IsDeleted && x.Chat.ConversationId == conversationId), ChatMessageDbService.GetDefaultSorting(Context)).AsEnumerable();
            return data.GroupBy(x => x.Chat).Select(g => new ConversationChats{Chat = g.Key, ChatMessages = g.ToList()}).ToList();

        }

        return baseData.ToList();
    }
    
    public async Task<List<ConversationChatsResponse>> GetChatsForConversationApi(DataQueryOptions? filter,int conversationId)
    {
        var chatFilterService = new QueryableDataService<ClientDbContext,ChatMessage>(Context);
        var mapper = new ConversationChatsToAPIMapper();
        // TODO: Is it better to use Chat and include chat messages for each chat
        if (filter == null)
        {
            var baseData = Context.Chats.Include(x => x.ChatMessages).Where(x => !x.IsDeleted && x.ConversationId == conversationId).Select(g=>mapper.MapNew(new ConversationChats{ Chat = g, ChatMessages = g.ChatMessages.ToList() }));
            return await baseData.ToListAsync();
        }
        else
        {
            var data = chatFilterService.ApplyDataQuery(filter, Context.ChatMessages.Include(x => x.Chat).Where(x => !x.Chat.IsDeleted && x.Chat.ConversationId == conversationId), ChatMessageDbService.GetDefaultSorting(Context)).AsEnumerable();
            return data.GroupBy(x => x.Chat).Select(g => mapper.MapNew(new ConversationChats{Chat = g.Key, ChatMessages = g.ToList()})).ToList();
            //baseData = Context.Chats.Include(x => x.ChatMessages).Where(x => !x.IsDeleted && x.ConversationId == conversationId).Select(g => mapper.MapNew(g, chatFilterService.ApplyFiltering(filter, g.ChatMessages, ChatMessageDbService.GetDefaultSorting(Context)).ToList()));

        }
    }
    /*
    public async Task<List<UserAiAgentChats>> GetAllChatsByAgentAndUser(DataFilter? filter,bool includeChatMessages)
    {
        IQueryable<Chat> baseData;
        baseData = filter == null ? Context.Chats : QueryableFilterService.ApplyFiltering(filter,Context.Chats,GetDefaultSorting());
        baseData = includeChatMessages ? baseData.Include(c => c.ChatMessages) : baseData;
        //var chats = await baseData.Where(x => x.AiAgentId != null).Include(x=>x.User.Phone).GroupBy(p => new UserAiAgent{ UserId = p.Uid, AiAgentId = p.AiAgentId.Value }).Select(g => new UserAiAgentChats { UserAiAgent = g.Key, Chats = g.ToList() }).ToListAsync();
        return null;
    }

    public async Task<List<UserAiAgentChatResponses>> GetAllChatsByAgentAndUserApi(DataFilter? filter,bool includeChatMessages)
    {
        var mapper = new ChatToApiMapper();

        IQueryable<Chat> baseData;
        if (filter == null)
            baseData = Context.Chats;
        else
            baseData = QueryableFilterService.ApplyFiltering(filter,Context.Chats,GetDefaultSorting());

        baseData = includeChatMessages ? baseData.Include(c => c.ChatMessages) : baseData;
        //var chats = await baseData.Where(x => x.AiAgentId != null).Include(x=>x.User.Phone).GroupBy(p => new UserAiAgent{  UserId = p.Uid, AiAgentId = p.AiAgentId.Value }).Select(g => new UserAiAgentChatResponses { UserAiAgent = g.Key, Chats = g.ToList().Select(x=>mapper.MapNew(x)).ToList() }).ToListAsync();
        return null;
    }*/
    
    public async Task<Chat?> GetChatByIdAsync(int id)
    {
        return await Context.Chats.Include(c => c.ChatMessages).FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<int> GetActiveAgent()
    {
        return await Context.Chats
            .Where(ch => !ch.IsDeleted
                      && ch.EndDate > DateTime.UtcNow)
            .Select(ch => ch.Conversation.AiAgentId)
            .Distinct()
            .CountAsync();
    }
    public async Task<int> GetInactiveAgent()
    {
  
        var activeAgentIds = Context.Chats
            .Where(ch => !ch.IsDeleted
                      && ch.EndDate > DateTime.UtcNow)
            .Select(ch => ch.Conversation.AiAgentId)
            .Distinct();

        return await Context.Conversations
            .Select(c => c.AiAgentId)
            .Distinct()
            .Except(activeAgentIds)
            .CountAsync();
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
        return nameof(Conversation.CreateDate);
    }
    
    public static List<string> GetDefaultSearchColumns(ClientDbContext context)
    {
        return new List<string>
        {

        };
    }
}