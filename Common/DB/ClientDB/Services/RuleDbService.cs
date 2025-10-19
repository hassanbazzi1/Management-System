using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class RuleDbService : DbFilterService<ClientDbContext,Rule>
{
    public RuleDbService(ClientDbContext context) : base(context)
    {
    }

    // Rule operations
    public async Task<List<Rule>> GetAllRulesAsync()
    {
        return await Context.Rules.ToListAsync();
    }

    public async Task<Rule?> GetRuleByIdAsync(int id)
    {
        return await Context.Rules.FindAsync(id);
    }

    public async Task AddRuleAsync(Rule rule)
    {
        await Context.Rules.AddAsync(rule);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateRuleAsync(Rule rule)
    {
        Context.Rules.Update(rule);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteRuleAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Rules.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Rules.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Rules.Update(entity);
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