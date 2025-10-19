using Common.DB.Common;
using Common.DB.MasterDB.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.MasterDB.Services;

public class IndustryDbService : DbService<MasterDbContext>
{
    public IndustryDbService(MasterDbContext context) : base(context)
    {
    }

    // Industries
    public async Task<List<Industry>> GetAllIndustriesAsync()
    {
        return await Context.Industries.ToListAsync();
    }

    public async Task<Industry?> GetIndustryByIdAsync(int id)
    {
        return await Context.Industries.FindAsync(id);
    }

    public async Task AddIndustryAsync(Industry industry)
    {
        await Context.Industries.AddAsync(industry);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateIndustryAsync(Industry industry)
    {
        Context.Industries.Update(industry);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteIndustryAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Industries.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Industries.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Industries.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }
}