using Common.API.ApiMapping.Users.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Users.Mappers;

[Mapper]
public partial class UserToApiMapper
{
    public partial UserResponse MapNew(User user);
}