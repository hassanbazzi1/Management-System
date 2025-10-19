using Common.API.ApiMapping.ChatReports.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.ChatReports.Mappers;

[Mapper]
public partial class ChatReportToApiMapper
{
    public partial ChatReportResponse MapNew(ChatReport report);
}