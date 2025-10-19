using Common.DB.Common;
using Common.DB.MasterDB.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.MasterDB.Services;

public class ClientUserDbService : DbService<MasterDbContext>
{
    public ClientUserDbService(MasterDbContext context) : base(context)
    {
    }

    // Client User

    public async Task<List<ClientUser>> GetAllClientUsersAsync()
    {
        return await Context.ClientUsers.ToListAsync();
    }

    public async Task<List<ClientUser>> GetClientUsersByTypeAsync(int typeId)
    {
        return await Context.ClientUsers.Where(t => t.TypeId == typeId).ToListAsync();
    }

    public async Task<ClientUser?> GetClientUserByIdAsync(int id)
    {
        return await Context.ClientUsers.FindAsync(id);
    }

    public async Task AddClientUserAsync(ClientUser user)
    {
        await Context.ClientUsers.AddAsync(user);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateClientUserAsync(ClientUser user)
    {
        Context.ClientUsers.Update(user);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteClientUserAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.ClientUsers.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.ClientUsers.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.ClientUsers.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }

    // Client User Type

    public async Task<List<ClientUserType>> GetAllClientUserTypesAsync()
    {
        return await Context.ClientUserTypes.ToListAsync();
    }

    public async Task<ClientUserType?> GetClientUserTypeByIdAsync(int id)
    {
        return await Context.ClientUserTypes.FindAsync(id);
    }

    public async Task AddClientUserTypeAsync(ClientUserType userType)
    {
        await Context.ClientUserTypes.AddAsync(userType);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateClientUserTypeAsync(ClientUserType userType)
    {
        Context.ClientUserTypes.Update(userType);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteClientUserTypeAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.ClientUserTypes.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.ClientUserTypes.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.ClientUserTypes.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }
}