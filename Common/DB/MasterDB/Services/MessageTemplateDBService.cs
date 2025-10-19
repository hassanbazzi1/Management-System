using Common.DB.Common;
using Common.DB.MasterDB.Models;
using Common.File;
using Common.Misc;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.MasterDB.Services;

public class MessageTemplateDbService : DbService<MasterDbContext>
{
    // TODO: Should be moved out of here
    public static readonly string[] MessageTemplates = new[]
    {
        "Master_Opening_Message", "Master_Agent_Created_Message", "Master_Create_Offer_Post_Verification_Message",
        "Master_Package_Finder_Message", "Master_Personalization_Message", "Master_Verification_Info_Message"
    };

    public MessageTemplateDbService(MasterDbContext context) : base(context)
    {
    }

    public async Task<List<AiAgentMessageTemplate>> GetAllAsync()
    {
        return await Context.MessageTemplates.ToListAsync();
    }

    public async Task<AiAgentMessageTemplate?> GetByIdAsync(string id)
    {
        return await Context.MessageTemplates.FindAsync(id);
    }

    public async Task AddAsync(AiAgentMessageTemplate msg)
    {
        await Context.MessageTemplates.AddAsync(msg);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AiAgentMessageTemplate msg)
    {
        Context.MessageTemplates.Update(msg);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var entity = await Context.MessageTemplates.FindAsync(id);
        if (entity == null)
            return false;

        Context.MessageTemplates.Remove(entity);
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<ActionResult> InitializeDefaultMessageTemplates()
    {
        var result = new ActionResult();
        try
        {
            await Context.MessageTemplates.ExecuteDeleteAsync();
            foreach (var message in MessageTemplates)
            {
                // TODO: Not hardcoded
                var data = FileUtil.ReadAllText("C:\\Users\\samij\\AI Agent\\AIChatbot\\Common\\DB\\Master\\Assets\\" + message + ".txt");
                await Context.MessageTemplates.AddAsync(new AiAgentMessageTemplate
                {
                    Name = message,
                    Text = data
                });
            }

            await Context.SaveChangesAsync();

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }
}