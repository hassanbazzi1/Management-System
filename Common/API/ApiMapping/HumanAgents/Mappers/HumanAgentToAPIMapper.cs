using Common.API.ApiMapping.HumanAgents.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.HumanAgents.Mappers;

[Mapper]
public partial class HumanAgentToApiMapper
{
    public partial HumanAgentResponse MapNew(HumanAgent agent);
}