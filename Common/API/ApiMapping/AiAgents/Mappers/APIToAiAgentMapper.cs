using Common.API.ApiMapping.AiAgents.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.AiAgents.Mappers;

[Mapper]
public partial class ApiToAiAgentMapper
{
    public partial AiAgent Map(AiAgentResponse agent);
}