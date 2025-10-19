using Common.API.ApiMapping.Instructions.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Instructions.Mappers;

[Mapper]
public partial class InstructionsToApiMapper
{
    public partial InstructionsResponse MapNew(global::Common.DB.ClientDB.Models.Instructions instructions);
}