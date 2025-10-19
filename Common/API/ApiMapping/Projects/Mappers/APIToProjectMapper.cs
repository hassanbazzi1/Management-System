using Common.API.ApiMapping.Projects.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Projects.Mappers;

[Mapper]
public partial class ApiToProjectMapper
{
    public partial Project MapNew(CreateUpdateProjectRequest project);

    public partial void MapExisting(CreateUpdateProjectRequest data, Project project);
}