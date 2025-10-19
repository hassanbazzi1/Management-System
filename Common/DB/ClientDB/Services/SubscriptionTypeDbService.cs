using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class SubscriptionTypeDbService : DbFilterService<ClientDbContext,SubscriptionType>
{
    public SubscriptionTypeDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<SubscriptionType>> GetAllSubscriptionTypesAsync()
    {
        return await Context.SubscriptionTypes.ToListAsync();
    }

    public async Task<SubscriptionType?> GetSubscriptionTypeByIdAsync(int id)
    {
        return await Context.SubscriptionTypes.FindAsync(id);
    }

    public async Task AddSubscriptionTypeAsync(SubscriptionType type)
    {
        await Context.SubscriptionTypes.AddAsync(type);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateSubscriptionTypeAsync(SubscriptionType type)
    {
        Context.SubscriptionTypes.Update(type);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteSubscriptionTypeAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.SubscriptionTypes.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.SubscriptionTypes.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.SubscriptionTypes.Update(entity);
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