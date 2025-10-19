using Microsoft.EntityFrameworkCore;

namespace Common.DB.Global;

public class GlobalDbContext : DbContext
{
    public GlobalDbContext()
    {
    }

    public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options)
    {
    }
}