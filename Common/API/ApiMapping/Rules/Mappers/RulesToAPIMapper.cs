using Common.API.ApiMapping.Rules.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Rules.Mappers;

[Mapper]
public partial class RulesToApiMapper
{
    public partial RuleResponse MapNew(Rule rule);
}