    using Common.API.ApiMapping.Storage.Models;
    using Common.DB.ClientDB.Options.Storage;
    using Common.DB.ClientDB.Services.Storage.Interfaces;
    using Microsoft.Extensions.Options;
    using Npgsql;
    using System;
    using System.Threading.Tasks;

    namespace Common.DB.ClientDB.Services.Storage.Implementations
    {
        public class PostgresStorageDbService : IStorageService
        {
            private readonly string _connString;

            public PostgresStorageDbService(
                IOptions<PostgreSqlSettings> opts)
            {
                _connString = opts.Value.ConnectionString
                    ?? throw new InvalidOperationException(
                         "PostgreSqlSettings:ConnectionString is missing.");
            }

            public async Task<StorageInfo> GetStorageInfo()
            {
                await using var conn = new NpgsqlConnection(_connString);
                await conn.OpenAsync();

                // Only pg_database_size is available on hosted servers:
                await using var cmd = new NpgsqlCommand(
                    "SELECT pg_database_size(current_database());", conn);
                var result = await cmd.ExecuteScalarAsync()
                             ?? throw new InvalidOperationException(
                                  "pg_database_size returned null.");

                var usedBytes = Convert.ToInt64(result);

                return new StorageInfo
                {
                    UsedBytes  = usedBytes,
                    TotalBytes = 0,     // not available on hosted
                    FreeBytes  = 0      // not available on hosted
                };
            }
        }
    }
