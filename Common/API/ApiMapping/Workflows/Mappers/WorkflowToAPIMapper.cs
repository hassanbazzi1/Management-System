using Common.API.ApiMapping.Workflows.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Workflows.Mappers;

[Mapper]
public partial class WorkflowToApiMapper
{
    public partial WorkflowResponse MapNew(Workflow workflow);
}