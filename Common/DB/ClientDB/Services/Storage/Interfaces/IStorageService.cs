using Common.API.ApiMapping.Storage.Models;

namespace Common.DB.ClientDB.Services.Storage.Interfaces
{
    public interface IStorageService
    {
        Task<StorageInfo> GetStorageInfo();
    }
}
