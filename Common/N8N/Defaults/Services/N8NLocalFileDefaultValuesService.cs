using Common.File;
using Common.Misc;
using Common.N8N.Defaults.Models;
using ActionResult = Common.Misc.ActionResult;


namespace Common.N8N.Defaults.Services;

public class N8NLocalFileDefaultValuesService : N8NDefaultValuesService
{
    public N8NLocalFileDefaultValuesService(string defaultWorkflowPath, string defaultSystemMessagePath, string defaultRulesPath, string defaultTemplateColumnTypePath, string defaultTemplateColumnPath)
    {
        DefaultWorkflowPath = defaultWorkflowPath;
        DefaultSystemMessagePath = defaultSystemMessagePath;
        DefaultRulesPath = defaultRulesPath;
        DefaultTemplateColumnTypePath = defaultTemplateColumnTypePath;
        DefaultTemplateColumnPath = defaultTemplateColumnPath;
    }

    private string DefaultWorkflowPath { get; }
    private string DefaultSystemMessagePath { get; }
    private string DefaultRulesPath { get; }
    private string DefaultTemplateColumnTypePath { get; }
    private string DefaultTemplateColumnPath { get; }

    public override async Task<DataActionResult<string>> GetDefaultWorkflowJson()
    {
        var result = new DataActionResult<string>();
        try
        {
            DefaultValues.DefaultWorkflowJson = FileUtil.ReadAllText(DefaultWorkflowPath);
            result.Data = DefaultValues.DefaultWorkflowJson;
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }

    public override async Task<DataActionResult<string>> GetDefaultSystemMessageJson()
    {
        var result = new DataActionResult<string>();
        try
        {
            DefaultValues.DefaultSystemMessageJson = FileUtil.ReadAllText(DefaultSystemMessagePath);
            result.Data = DefaultValues.DefaultWorkflowJson;
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }

    public override async Task<DataActionResult<string>> GetDefaultRulesJson()
    {
        var result = new DataActionResult<string>();
        try
        {
            DefaultValues.DefaultRulesJson = FileUtil.ReadAllText(DefaultRulesPath);
            result.Data = DefaultValues.DefaultWorkflowJson;
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }

    public override async Task<DataActionResult<string>> GetDefaultTemplateColumnTypeJson()
    {
        var result = new DataActionResult<string>();
        try
        {
            DefaultValues.DefaultTemplateColumnTypeJson = FileUtil.ReadAllText(DefaultTemplateColumnTypePath);
            result.Data = DefaultValues.DefaultWorkflowJson;
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }

    public override async Task<DataActionResult<string>> GetDefaultTemplateColumnJson()
    {
        var result = new DataActionResult<string>();
        try
        {
            DefaultValues.DefaultTemplateColumnJson = FileUtil.ReadAllText(DefaultTemplateColumnPath);
            result.Data = DefaultValues.DefaultWorkflowJson;
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }

    public override async Task<DataActionResult<N8NDefaultValues>> GetAllDefaultValues()
    {
        var defaultValuesResult = new DataActionResult<N8NDefaultValues>();
        var workflowTask = GetDefaultWorkflowJson();
        var systemMessageTask = GetDefaultSystemMessageJson();
        var rulesTask = GetDefaultRulesJson();
        var templateColumnTypeTask = GetDefaultTemplateColumnTypeJson();
        var templateColumnTask = GetDefaultTemplateColumnJson();

        var results = await Task.WhenAll(workflowTask, systemMessageTask, rulesTask, templateColumnTypeTask, templateColumnTask);
        defaultValuesResult.Status = ActionResult.ActionStatus.Success;
        foreach (var result in results)
            if (result.Status != ActionResult.ActionStatus.Success)
            {
                defaultValuesResult.Status = ActionResult.ActionStatus.Failure;
                defaultValuesResult.AddChildResult(result);
            }

        defaultValuesResult.Data = DefaultValues;
        return defaultValuesResult;
    }
}