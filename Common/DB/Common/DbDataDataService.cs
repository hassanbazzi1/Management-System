using Common.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.Common;

public abstract class DbDataDataService<T,F> where T:DbContext
{
    protected T _context;
    
    protected DbDataDataService(T context)
    {
        _context = context;
    }

    protected Type? GetTypeOfEntity(string propertyName)
    {
        var entityType = _context.Model.FindEntityType(typeof(F));
        var property = entityType?.GetProperty(propertyName);
        return property?.ClrType;
    }
}