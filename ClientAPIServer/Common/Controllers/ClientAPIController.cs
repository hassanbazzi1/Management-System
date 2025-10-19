using Common.API.Models;
using Common.DB.ClientDB;

namespace ClientAPIServer.Common.Controllers;

public class ClientAPIController : ApiController<ClientDbContext>
{
    public ClientAPIController(ClientDbContext dbContext) : base(dbContext)
    {
    }
}