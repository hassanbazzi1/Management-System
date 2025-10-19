using Common.API.ApiMapping.Conversations.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Conversations.Mappers;

[Mapper]
public partial class ChatToApiMapper
{
    public partial ChatResponse MapNew(Chat chat);
}