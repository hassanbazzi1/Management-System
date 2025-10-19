using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class WorkflowTemplateDbService : DbFilterService<ClientDbContext,WorkflowTemplate>
{
    public WorkflowTemplateDbService(ClientDbContext context) : base(context)
    {
    }
    
    public async Task<List<WorkflowTemplate>> GetAllWorkflowTemplatesAsync()
    {
        return await Context.WorkflowTemplates.ToListAsync();
    }

    public async Task<WorkflowTemplate> GetWorkflowTemplateByIdAsync(int id)
    {
        return await Context.WorkflowTemplates.FindAsync(id);
    }

    public async Task AddWorkflowTemplateAsync(WorkflowTemplate template)
    {
        await Context.WorkflowTemplates.AddAsync(template);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateWorkflowTemplateAsync(WorkflowTemplate template)
    {
        Context.WorkflowTemplates.Update(template);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteWorkflowTemplateAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.WorkflowTemplates.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.WorkflowTemplates.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.WorkflowTemplates.Update(entity);
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