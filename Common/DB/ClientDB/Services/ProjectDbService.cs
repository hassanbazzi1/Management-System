using Common.API.ApiMapping.Analytics.Mappers;
using Common.API.ApiMapping.Projects.Models;
using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ProjectDbService : DbFilterService<ClientDbContext,Project>
{

    public ProjectDbService(ClientDbContext context) : base(context)
    {
        
    }

    public async Task<List<Project>> GetAllAsync(bool includeWorkflows = false)
    {
        if (!includeWorkflows)
            return await Context.Projects.ToListAsync();
        return await Context.Projects.Include(p => p.Workflows).ThenInclude(w => w.Subscription).ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await Context.Projects.FindAsync(id);
    }

    public async Task AddAsync(Project project)
    {
        await Context.Projects.AddAsync(project);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Project project)
    {
        Context.Projects.Update(project);
        await Context.SaveChangesAsync();
    }
   
    public async Task<bool> DeleteAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Projects.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Projects.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Projects.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }
    public async Task<FilteredStatsResult> GetFilteredStatsAsync(FilteredStatsRequest req)
    {
        int month = req.Month ?? DateTime.UtcNow.Month;
        int year = DateTime.UtcNow.Year;
        int page = req.PageId;

        // 1) Workflows for this page
        var workflowIds = await Context.Workflows
            .Where(w => !w.IsDeleted && w.ProjectId == page)
            .Select(w => w.Id)
            .ToListAsync();

        // 2) Conversations under those workflows (and agent filter, if any)
        var convoQuery = Context.Conversations
            .Where(c => !c.IsDeleted &&
                        c.AiAgent != null &&
                        workflowIds.Contains(c.AiAgent!.Workflow.Id));

        if (req.AgentId.HasValue)
            convoQuery = convoQuery.Where(c => c.AiAgentId == req.AgentId.Value);

        // 3) Sessions for that convo set in the chosen month
        var sessionsQuery = Context.Sessions
            .Where(s => !s.IsDeleted &&
                        s.StartDate.Month == month &&
                        s.StartDate.Year  == year &&
                        convoQuery.Any(c => c.Id == s.ConversationId));

        int sessions = await sessionsQuery.CountAsync();

        // 4) Messages tied to those sessions
        int messages = await Context.ChatMessages
            .Where(cm => !cm.IsDeleted &&
                         cm.Chat.Conversation.Sessions.Any(sess =>
                             sess.Id == cm.Chat.Conversation.Sessions
                                              .OrderBy(x => x.StartDate).First().Id &&
                             sess.StartDate.Month == month &&
                             sess.StartDate.Year  == year &&
                             convoQuery.Any(c => c.Id == sess.ConversationId)))
            .CountAsync();

        var _mapper = new FilteredStatsMapper();
        return _mapper.Map((sessions, messages), req, month);
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