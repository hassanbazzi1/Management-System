using Microsoft.EntityFrameworkCore;

namespace Common.DB.Common;

public abstract class DbService<T> where T:DbContext
{
    public DbService(T context)
    {
        Context = context;
    }

    public T Context { get; }
    
}