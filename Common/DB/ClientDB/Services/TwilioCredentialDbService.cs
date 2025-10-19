using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class TwilioCredentialDbService : DbFilterService<ClientDbContext,TwilioCredential>
{
    public TwilioCredentialDbService(ClientDbContext context) : base(context)
    {
    }

    public async Task<List<TwilioCredential>> GetAllTwilioCredentialsAsync()
    {
        return await Context.TwilioCredentials.ToListAsync();
    }

    public async Task<TwilioCredential?> GetTwilioCredentialByIdAsync(int id)
    {
        return await Context.TwilioCredentials.FindAsync(id);
    }

    public async Task AddTwilioCredentialAsync(TwilioCredential credential)
    {
        await Context.TwilioCredentials.AddAsync(credential);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateTwilioCredentialAsync(TwilioCredential credential)
    {
        Context.TwilioCredentials.Update(credential);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteTwilioCredentialAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.TwilioCredentials.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.TwilioCredentials.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.TwilioCredentials.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }

    // Google Credential Methods

    public async Task<List<GoogleCredential>> GetAllGoogleCredentialsAsync()
    {
        return await Context.GoogleCredentials.ToListAsync();
    }

    public async Task<GoogleCredential?> GetGoogleCredentialByIdAsync(int id)
    {
        return await Context.GoogleCredentials.FindAsync(id);
    }

    public async Task AddGoogleCredentialAsync(GoogleCredential credential)
    {
        await Context.GoogleCredentials.AddAsync(credential);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateGoogleCredentialAsync(GoogleCredential credential)
    {
        Context.GoogleCredentials.Update(credential);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteGoogleCredentialAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.GoogleCredentials.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.GoogleCredentials.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.GoogleCredentials.Update(entity);
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