using Common.Misc;
using Common.N8N.Defaults.Models;

namespace Common.N8N.Defaults.Services;

public abstract class N8NDefaultValuesService
{
    public N8NDefaultValuesService()
    {
        DefaultValues = new N8NDefaultValues();
    }

    protected N8NDefaultValuesService(string companyNamePlaceholder, string companyServicesPlaceholder, string companyWebsitePlaceholder)
    {
        DefaultValues = new N8NDefaultValues(companyNamePlaceholder, companyServicesPlaceholder, companyWebsitePlaceholder);
    }

    public N8NDefaultValues? DefaultValues { get; }

    public bool HasDefaultValues()
    {
        return DefaultValues != null && DefaultValues.DefaultWorkflowJson != null && DefaultValues.DefaultSystemMessageJson != null && DefaultValues.DefaultRulesJson != null && DefaultValues.DefaultTemplateColumnTypeJson != null && DefaultValues.DefaultTemplateColumnJson != null;
    }

    public abstract Task<DataActionResult<string>> GetDefaultWorkflowJson();
    public abstract Task<DataActionResult<string>> GetDefaultSystemMessageJson();
    public abstract Task<DataActionResult<string>> GetDefaultRulesJson();
    public abstract Task<DataActionResult<string>> GetDefaultTemplateColumnTypeJson();
    public abstract Task<DataActionResult<string>> GetDefaultTemplateColumnJson();

    public abstract Task<DataActionResult<N8NDefaultValues>> GetAllDefaultValues();
}