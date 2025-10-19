using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class InstructionsDbService : DbFilterService<ClientDbContext,Instructions>
{
    public InstructionsDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<Instructions>> GetAllInstructionsAsync()
    {
        return await Context.Instructions.ToListAsync();
    }

    public async Task<Instructions?> GetInstructionsByIdAsync(int id)
    {
        return await Context.Instructions.FindAsync(id);
    }

    public async Task AddInstructionsAsync(Instructions instruction)
    {
        await Context.Instructions.AddAsync(instruction);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateInstructionsAsync(Instructions instruction)
    {
        Context.Instructions.Update(instruction);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteInstructionsAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Instructions.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Instructions.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Instructions.Update(entity);
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