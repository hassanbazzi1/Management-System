using Common.API.ApiMapping.Industries.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Industries.Mappers;

[Mapper]
public partial class IndustryToApiMapper
{
    public partial IndustryResponse MapNew(Industry industry);
}