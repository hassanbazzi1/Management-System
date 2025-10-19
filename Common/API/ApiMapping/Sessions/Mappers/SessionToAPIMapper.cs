using Common.API.ApiMapping.Sessions.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Sessions.Mappers;

[Mapper]
public partial class SessionToApiMapper
{
    public partial SessionResponse MapNew(Session session);
}