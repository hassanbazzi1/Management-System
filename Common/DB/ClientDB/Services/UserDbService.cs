using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class UserDbService : DbFilterService<ClientDbContext,User>
{
    public UserDbService(ClientDbContext context) : base(context)
    {
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await Context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await Context.Users.FindAsync(id);
    }

    public async Task AddAsync(User user)
    {
        await Context.Users.AddAsync(user);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        Context.Users.Update(user);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Users.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Users.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Users.Update(entity);
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