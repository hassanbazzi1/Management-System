using Common.API.ApiMapping.Sessions.Mappers;
using Common.API.ApiMapping.Sessions.Models;
using Common.DB.ClientDB.Models;
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Functions.Coth;
using Polly;

namespace Common.DB.ClientDB.Services;

public class SessionDbService : DbFilterService<ClientDbContext,Session>
{
    
    public SessionDbService(ClientDbContext context ) : base(context)
    {

 
    }

    public async Task<List<Session>> GetAllAsync()
    {
        return await Context.Sessions.ToListAsync();
    }

    public async Task<Session?> GetByIdAsync(int id)
    {
        return await Context.Sessions.FindAsync(id);
    }

    public async Task AddAsync(Session session)
    {
        await Context.Sessions.AddAsync(session);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Session session)
    {
        Context.Sessions.Update(session);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, bool dbDelete = false)
    {
        var entity = await Context.Sessions.FindAsync(id);
        if (entity == null)
            return false;

        if (dbDelete)
        {
            Context.Sessions.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
            Context.Sessions.Update(entity);
        }

        await Context.SaveChangesAsync();
        return true;
    }
    public async Task<int> GetAllSessions()
    {
        var result=  await Context.Sessions.CountAsync();

        return result;
    }
   
    public async Task<int> GetActiveSessions()
    {
        // Use UtcNow if you store dates in UTC; otherwise use DateTime.Now
        var now = DateTime.UtcNow;

        return await Context.Sessions
            .Where(s =>
                !s.IsDeleted &&
                s.StartDate <= now &&
                s.EndDate >= now
            )

            .CountAsync();
    }

    public async Task<int[]> GetMonthlyCountsAsync(int year)
    {
        var raw = await Context.Sessions
        .Where(s => !s.IsDeleted && s.StartDate.Year == year)
        .GroupBy(s => s.StartDate.Month)
        .Select(g => new { Month = g.Key, Count = g.Count() })
        .ToDictionaryAsync(x => x.Month, x => x.Count);

        var counts = new int[12];
        for (int m = 1; m <= 12; m++)
            raw.TryGetValue(m, out counts[m - 1]);

        return counts;
    }
    public async Task<MonthlySessionsResponse> GetYearlyComparisonAsync()
    {
        var year = DateTime.UtcNow.Year;
        var cur = await GetMonthlyCountsAsync(year);
        var pst = await GetMonthlyCountsAsync(year - 1);
        var _mapper = new MonthsToYearMapper();
        return _mapper.Map(cur,pst);
    }
    public async Task<int> GetClosedSessions()
{
    // Use UtcNow if your dates are stored in UTC; otherwise use DateTime.Now
    var now = DateTime.UtcNow;

    return await Context.Sessions
        .Where(s =>
            !s.IsDeleted &&
            s.EndDate < now
        )
        .CountAsync();
}

public static string GetDefaultSorting(ClientDbContext context)
    {
        throw new NotImplementedException();
    }

    public static List<string> GetDefaultSearchColumns(ClientDbContext context)
    {
        throw new NotImplementedException();
    }
}