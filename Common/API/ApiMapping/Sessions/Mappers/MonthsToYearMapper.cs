using Common.API.ApiMapping.Sessions.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Sessions.Mappers;


public  class MonthsToYearMapper
{
    public MonthlySessionsResponse Map(int[] current, int[] previous)
        => new MonthlySessionsResponse
        {
            current  = current,
            previous = previous
        };
}

