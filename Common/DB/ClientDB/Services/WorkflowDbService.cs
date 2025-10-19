using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class WorkflowDbService : DbFilterService<ClientDbContext,Workflow>
{
    public WorkflowDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<Workflow>> GetAllWorkflowsAsync()
    {
        return await Context.Workflows.ToListAsync();
    }

    public async Task<Workflow?> GetWorkflowByIdAsync(int id)
    {
        return await Context.Workflows.FindAsync(id);
    }

    public async Task AddWorkflowAsync(Workflow workflow)
    {
        await Context.Workflows.AddAsync(workflow);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateWorkflowAsync(Workflow workflow)
    {
        Context.Workflows.Update(workflow);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteWorkflowAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Workflows.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Workflows.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Workflows.Update(entity);
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