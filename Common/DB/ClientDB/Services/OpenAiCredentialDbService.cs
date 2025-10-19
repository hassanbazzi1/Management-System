using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class OpenAiCredentialDbService : DbFilterService<ClientDbContext,OpenAiCredential>
{
    public OpenAiCredentialDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<OpenAiCredential>> GetAllOpenAiCredentialsAsync()
    {
        return await Context.OpenaiCredentials.ToListAsync();
    }

    public async Task<OpenAiCredential?> GetOpenAiCredentialByIdAsync(int id)
    {
        return await Context.OpenaiCredentials.FindAsync(id);
    }

    public async Task AddOpenAiCredentialAsync(OpenAiCredential credential)
    {
        await Context.OpenaiCredentials.AddAsync(credential);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateOpenAiCredentialAsync(OpenAiCredential credential)
    {
        Context.OpenaiCredentials.Update(credential);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteOpenAiCredentialAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.OpenaiCredentials.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.OpenaiCredentials.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.OpenaiCredentials.Update(entity);
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