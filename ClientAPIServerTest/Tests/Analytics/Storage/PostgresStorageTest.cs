using System;
using System.Threading.Tasks;
using Common.API.ApiMapping.Storage.Models;
using Common.DB.ClientDB.Options.Storage;
using Common.DB.ClientDB.Services.Storage.Implementations;
using Microsoft.Extensions.Options;
using Xunit;

namespace ClientAPIServerTest.Tests.Analytics.Storage
{
    public class PostgresStorageDbServiceTests
    {
        // Helper for IOptions<>
        private static IOptions<PostgreSqlSettings> Opts(string cs) =>
            Options.Create(new PostgreSqlSettings { ConnectionString = cs });

        [Fact]
        public void Ctor_WithNullConnectionString_Throws()
        {
            Assert.Throws<InvalidOperationException>(
                () => new PostgresStorageDbService(Opts(null!)));
        }

        [Fact]
        public void Ctor_WithValidConnectionString_DoesNotThrow()
        {
            var cs = "Host=localhost;Port=5432;Username=postgres;Password=postgres";
            var ex = Record.Exception(() => new PostgresStorageDbService(Opts(cs)));
            Assert.Null(ex);
        }

        [Fact]
        public async Task GetStorageInfo_LocalPostgres_ReturnsNonNegativeUsedBytes()
        {
            // ⚠️ MUST match the JSON exactly
            var cs = "Host=localhost;Port=5432;Username=postgres;Password=postgres";

            var svc = new PostgresStorageDbService(Opts(cs));

            StorageInfo info = await svc.GetStorageInfo();

            Assert.NotNull(info);
            Assert.True(info.UsedBytes >= 0);
            Assert.Equal(0, info.TotalBytes);
            Assert.Equal(0, info.FreeBytes);
        }
    }
}
