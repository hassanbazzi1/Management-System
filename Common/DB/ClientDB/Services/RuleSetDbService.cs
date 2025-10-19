using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class RuleSetDbService : DbFilterService<ClientDbContext,RuleSet>
{
    public RuleSetDbService(ClientDbContext context) : base(context)
    {
    }

    // RuleSet operations
    public async Task<List<RuleSet>> GetAllRuleSetsAsync()
    {
        return await Context.RuleSets.ToListAsync();
    }

    public async Task<RuleSet?> GetRuleSetByIdAsync(int id)
    {
        return await Context.RuleSets.FindAsync(id);
    }

    public async Task AddRuleSetAsync(RuleSet ruleSet)
    {
        await Context.RuleSets.AddAsync(ruleSet);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateRuleSetAsync(RuleSet ruleSet)
    {
        Context.RuleSets.Update(ruleSet);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteRuleSetAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.RuleSets.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.RuleSets.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.RuleSets.Update(entity);
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