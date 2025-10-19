using Common.API.ApiMapping.Projects.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Projects.Mappers;

[Mapper]
public partial class ProjectToApiMapper
{
    public partial ProjectResponse MapNew(Project project);
}