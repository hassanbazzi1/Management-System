using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class RuleSetRuleDbService : DbFilterService<ClientDbContext,RuleSetRule>
{
    public RuleSetRuleDbService(ClientDbContext context) : base(context)
    {
    }
    
    // RuleSetRule operations
    public async Task<List<RuleSetRule>> GetAllRuleSetRulesAsync()
    {
        return await Context.RuleSetRules.ToListAsync();
    }

    public async Task<List<RuleSetRule>> GetAllRuleSetsByRuleSetIdAsync(int rsid)
    {
        return await Context.RuleSetRules.Where(m => m.Rsid == rsid).ToListAsync();
    }

    public async Task<RuleSetRule?> GetRuleSetRuleByIdAsync(int id)
    {
        return await Context.RuleSetRules.FindAsync(id);
    }

    public async Task AddRuleSetRuleAsync(RuleSetRule ruleSetRule)
    {
        await Context.RuleSetRules.AddAsync(ruleSetRule);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateRuleSetRuleAsync(RuleSetRule ruleSetRule)
    {
        Context.RuleSetRules.Update(ruleSetRule);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteRuleSetRuleAsync(int id)
    {
        var entity = await Context.RuleSetRules.FindAsync(id);
        if (entity == null)
            return false;

        Context.RuleSetRules.Remove(entity);
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