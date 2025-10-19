using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class InstructionsRuleDBService : DbFilterService<ClientDbContext,InstructionsRule>
{
    public InstructionsRuleDBService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<InstructionsRule>> GetAllRulesAsync()
    {
        return await Context.InstructionsRules.ToListAsync();
    }

    public async Task<InstructionsRule?> GetRuleByIdAsync(int id)
    {
        return await Context.InstructionsRules.FindAsync(id);
    }

    public async Task AddRuleAsync(InstructionsRule rule)
    {
        await Context.InstructionsRules.AddAsync(rule);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateRuleAsync(InstructionsRule rule)
    {
        Context.InstructionsRules.Update(rule);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteRuleAsync(int id)
    {
        var entity = await Context.InstructionsRules.FindAsync(id);
        if (entity == null)
            return false;

        Context.InstructionsRules.Remove(entity);
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