using Common.DB.Common;
using Common.DB.MasterDB.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.MasterDB.Services;

public class SettingDbService : DbService<MasterDbContext>
{
    public SettingDbService(MasterDbContext context) : base(context)
    {
    }

    // Setting
    public async Task<List<Setting>> GetAllSettingsAsync()
    {
        return await Context.Settings.ToListAsync();
    }

    public async Task<Setting?> GetSettingByIdAsync(int id)
    {
        return await Context.Settings.FindAsync(id);
    }

    public async Task AddSettingAsync(Setting setting)
    {
        await Context.Settings.AddAsync(setting);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateSettingAsync(Setting setting)
    {
        Context.Settings.Update(setting);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteSettingAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Settings.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Settings.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Settings.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }

    // ClientSetting
    public async Task<List<ClientSetting>> GetAllClientSettingsAsync()
    {
        return await Context.ClientSettings.ToListAsync();
    }

    public async Task<ClientSetting?> GetClientSettingByIdAsync(int id)
    {
        return await Context.ClientSettings.FindAsync(id);
    }

    public async Task AddClientSettingAsync(ClientSetting setting)
    {
        await Context.ClientSettings.AddAsync(setting);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateClientSettingAsync(ClientSetting setting)
    {
        Context.ClientSettings.Update(setting);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteClientSettingAsync(int id)
    {
        var setting = await Context.ClientSettings.FindAsync(id);
        if (setting == null)
            return false;

        Context.ClientSettings.Remove(setting);
        await Context.SaveChangesAsync();
        return true;
    }
}