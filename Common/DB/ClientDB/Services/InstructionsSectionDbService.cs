using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class InstructionsSectionDbService : DbFilterService<ClientDbContext,InstructionsSection>
{
    public InstructionsSectionDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<InstructionsSection>> GetAllSectionsAsync()
    {
        return await Context.InstructionsSections.ToListAsync();
    }

    public async Task<InstructionsSection?> GetSectionByIdAsync(int id)
    {
        return await Context.InstructionsSections.FindAsync(id);
    }

    public async Task<List<InstructionsSection>> GetSectionsByInstructionIdAsync(int iid)
    {
        return await Context.InstructionsSections.Where(m => m.Iid == iid).ToListAsync();
    }

    public async Task AddSectionAsync(InstructionsSection section)
    {
        await Context.InstructionsSections.AddAsync(section);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateSectionAsync(InstructionsSection section)
    {
        Context.InstructionsSections.Update(section);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteSectionAsync(int id, bool dbDelete = false)

    {
        var entity = await Context.InstructionsSections.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.InstructionsSections.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.InstructionsSections.Update(entity);
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