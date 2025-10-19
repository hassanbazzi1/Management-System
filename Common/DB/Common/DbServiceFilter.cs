using Microsoft.EntityFrameworkCore;

namespace Common.DB.Common;

public abstract class DbFilterService<T,F> : DbService<T> where T:DbContext
{
    protected QueryableDataService<T,F> QueryableDataService { get; set; }

    public DbFilterService(T context) : base(context)
    {
        QueryableDataService = new QueryableDataService<T,F>(context);
    }
    
}