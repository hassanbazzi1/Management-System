using Common.API.ApiMapping.Sessions.Mappers;
using Common.API.Models;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Models;
using Common.DB.ClientDB.Services;    // for SessionDbService
using Common.DB.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClientAPIServerTest.Tests.SessionTest
{
    public class SessionTest
    {
        private ClientDbContext CreateContext(string dbName)
        {
            var opts = new DbContextOptionsBuilder<ClientDbContext>()
                       .UseInMemoryDatabase(dbName)
                       .Options;
            return new ClientDbContext(opts);
        }

        private static Session MakeSession(DateTime start, DateTime end, bool deleted = false) =>
            new() { StartDate = start, EndDate = end, IsDeleted = deleted };

        [Fact]
        public async Task GetYearlyComparisonAsync_ReturnsExpectedCounts()
        {
            // Arrange
            var utcNow = new DateTime(2025, 5, 26);     // fixed “today”
            var thisYear = utcNow.Year;                   // 2025
            var lastYear = thisYear - 1;                  // 2024

            var ctx = CreateContext(nameof(GetYearlyComparisonAsync_ReturnsExpectedCounts));

            // 3 sessions in Jan-Mar 2025, 2 sessions in Apr-May 2024
            ctx.Sessions.AddRange(
                MakeSession(new DateTime(thisYear, 1, 1), utcNow),
                MakeSession(new DateTime(thisYear, 2, 10), utcNow),
                MakeSession(new DateTime(thisYear, 3, 20), utcNow),
                MakeSession(new DateTime(lastYear, 4, 5), utcNow),
                MakeSession(new DateTime(lastYear, 5, 18), utcNow)
            );
            await ctx.SaveChangesAsync();

            var mapper = new MonthsToYearMapper();          // your hand-written mapper
            var service = new SessionDbService(ctx, mapper);

            // Act
            var dto = await service.GetYearlyComparisonAsync();

            // Assert – counts[month-1]
            Assert.Equal(3, dto.current.Take(3).Sum());       // Jan-Mar 2025
            Assert.Equal(0, dto.current.Skip(3).Sum());       // rest of 2025
            Assert.Equal(2, dto.previous.Skip(3).Take(2).Sum());  // Apr-May 2024
        }

        [Fact]
        public async Task GetActiveSessions_OnlyCountsLiveOnes()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var ctx = CreateContext(nameof(GetActiveSessions_OnlyCountsLiveOnes));

            // Active
            ctx.Sessions.Add(MakeSession(now.AddHours(-1), now.AddHours(+1)));
            // Started but ended yesterday (closed)
            ctx.Sessions.Add(MakeSession(now.AddDays(-2), now.AddDays(-1)));
            // Deleted
            ctx.Sessions.Add(MakeSession(now.AddHours(-1), now.AddHours(+1), deleted: true));

            await ctx.SaveChangesAsync();

            var service = new SessionDbService(ctx, new MonthsToYearMapper());

            // Act
            var activeCount = await service.GetActiveSessions();

            // Assert
            Assert.Equal(1, activeCount);
        }

        [Fact]
        public async Task GetClosedSessions_OnlyCountsPastOnes()
        {
            var now = DateTime.UtcNow;
            var ctx = CreateContext(nameof(GetClosedSessions_OnlyCountsPastOnes));

            ctx.Sessions.Add(MakeSession(now.AddDays(-3), now.AddDays(-2))); // closed
            ctx.Sessions.Add(MakeSession(now.AddDays(-1), now.AddHours(+2))); // active
            await ctx.SaveChangesAsync();

            var service = new SessionDbService(ctx, new MonthsToYearMapper());

            var closed = await service.GetClosedSessions();

            Assert.Equal(1, closed);
        }
    }
}
