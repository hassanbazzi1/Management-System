using Common.API.Models;
using Common.DB.MasterDB;

namespace MainServer.Common.Controllers;

public class MainServerController : ApiController<MasterDbContext>
{
    public MainServerController(MasterDbContext dbContext) : base(dbContext)
    {
    }
}