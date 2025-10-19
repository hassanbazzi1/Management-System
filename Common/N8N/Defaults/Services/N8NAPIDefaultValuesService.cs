using Common.Misc;
using Common.N8N.Defaults.Models;

namespace Common.N8N.Defaults.Services;

public class N8NapiDefaultValuesService : N8NDefaultValuesService
{
    public override async Task<DataActionResult<string>> GetDefaultWorkflowJson()
    {
        throw new NotImplementedException();
    }

    public override async Task<DataActionResult<string>> GetDefaultSystemMessageJson()
    {
        throw new NotImplementedException();
    }

    public override async Task<DataActionResult<string>> GetDefaultRulesJson()
    {
        throw new NotImplementedException();
    }

    public override async Task<DataActionResult<string>> GetDefaultTemplateColumnTypeJson()
    {
        throw new NotImplementedException();
    }

    public override async Task<DataActionResult<string>> GetDefaultTemplateColumnJson()
    {
        throw new NotImplementedException();
    }

    public override async Task<DataActionResult<N8NDefaultValues>> GetAllDefaultValues()
    {
        throw new NotImplementedException();
    }
}