using Common.Misc;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.ClientDB.Services;

public class ClientTemplateDbService
{
    private readonly ClientDbContext _templateContext;

    public ClientTemplateDbService(ClientDbContext templateContext)
    {
        _templateContext = templateContext;
    }

    public async Task<DataActionResult<string>> ClearClientTemplateDatabase()
    {
        var result = new DataActionResult<string>();
        try
        {
            await _templateContext.AiAgents.ExecuteDeleteAsync();
            await _templateContext.ChatMessages.ExecuteDeleteAsync();
            await _templateContext.ChatReportColumnValues.ExecuteDeleteAsync();
            await _templateContext.ChatReports.ExecuteDeleteAsync();
            await _templateContext.ChatReportInstancesGoogleSheets.ExecuteDeleteAsync();
            await _templateContext.Chats.ExecuteDeleteAsync();
            await _templateContext.ChatReportInstances.ExecuteDeleteAsync();
            await _templateContext.ChatReportTemplateColumns.ExecuteDeleteAsync();
            await _templateContext.ChatReportTemplateColumnTypes.ExecuteDeleteAsync();
            await _templateContext.ChatReportTemplates.ExecuteDeleteAsync();
            await _templateContext.ChatReportFormats.ExecuteDeleteAsync();
            await _templateContext.GoogleCredentials.ExecuteDeleteAsync();
            await _templateContext.HumanAgents.ExecuteDeleteAsync();
            await _templateContext.InstructionsRules.ExecuteDeleteAsync();
            await _templateContext.InstructionsSections.ExecuteDeleteAsync();
            await _templateContext.Instructions.ExecuteDeleteAsync();
            await _templateContext.OpenaiCredentials.ExecuteDeleteAsync();
            await _templateContext.RuleSetRules.ExecuteDeleteAsync();
            await _templateContext.RuleSets.ExecuteDeleteAsync();
            await _templateContext.Rules.ExecuteDeleteAsync();
            await _templateContext.Sessions.ExecuteDeleteAsync();
            await _templateContext.Users.ExecuteDeleteAsync();
            await _templateContext.TwilioCredentials.ExecuteDeleteAsync();
            await _templateContext.Workflows.ExecuteDeleteAsync();
            await _templateContext.WorkflowTemplates.ExecuteDeleteAsync();
            await _templateContext.Subscriptions.ExecuteDeleteAsync();
            await _templateContext.SubscriptionTypes.ExecuteDeleteAsync();
            await _templateContext.Projects.ExecuteDeleteAsync();
            await _templateContext.Industries.ExecuteDeleteAsync();
            await _templateContext.SaveChangesAsync();

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Message = e.Message;
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }

    public async Task<DataActionResult<string>> CloneDatabaseFromTemplate(ClientDbContext targetContext)
    {
        var result = new DataActionResult<string>();
        try
        {
            var instructions = await _templateContext.Instructions.ToListAsync();
            var instructionsSection = await _templateContext.InstructionsSections.ToListAsync();
            var rules = await _templateContext.Rules.ToListAsync();
            var rulesets = await _templateContext.RuleSets.ToListAsync();
            var rulesetrules = await _templateContext.RuleSetRules.ToListAsync();
            var instructionRules = await _templateContext.InstructionsRules.ToListAsync();
            var chatReportFormats = await _templateContext.ChatReportFormats.ToListAsync();
            var chatReportTemplates = await _templateContext.ChatReportTemplates.ToListAsync();
            var chatReportTemplateColumns = await _templateContext.ChatReportTemplateColumns.ToListAsync();
            var chatReportTemplateColumnTypes = await _templateContext.ChatReportTemplateColumnTypes.ToListAsync();
            var industries = await _templateContext.Industries.ToListAsync();
            var workflowTemplates = await _templateContext.WorkflowTemplates.ToListAsync();
            var subscriptionTypes = await _templateContext.SubscriptionTypes.ToListAsync();

            await targetContext.Instructions.AddRangeAsync(instructions);
            await targetContext.InstructionsSections.AddRangeAsync(instructionsSection);
            await targetContext.Rules.AddRangeAsync(rules);
            await targetContext.RuleSets.AddRangeAsync(rulesets);
            await targetContext.InstructionsRules.AddRangeAsync(instructionRules);
            await targetContext.RuleSetRules.AddRangeAsync(rulesetrules);
            await targetContext.Industries.AddRangeAsync(industries);
            await targetContext.SubscriptionTypes.AddRangeAsync(subscriptionTypes);
            await targetContext.WorkflowTemplates.AddRangeAsync(workflowTemplates);
            await targetContext.ChatReportFormats.AddRangeAsync(chatReportFormats);
            await targetContext.ChatReportTemplateColumnTypes.AddRangeAsync(chatReportTemplateColumnTypes);
            await targetContext.ChatReportTemplates.AddRangeAsync(chatReportTemplates);
            await targetContext.ChatReportTemplateColumns.AddRangeAsync(chatReportTemplateColumns);
            await targetContext.SaveChangesAsync();

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Message = e.Message;
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }

    public async Task<DataActionResult<string>> InitializeClientTemplateDatabase()
    {
        var result = new DataActionResult<string>();
        try
        {
            // TODO: Implement
        }
        catch (Exception e)
        {
            result.Message = e.Message;
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }
}