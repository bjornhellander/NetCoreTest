using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Items
{
    public interface IItemRepositoryService
    {
        Task<List<ItemRepositoryData>> GetAllItemsAsync(DbConnection connection, DbTransaction transaction);

        Task<List<int>> CreateItemsAsync(DbConnection connection, DbTransaction transaction, List<ItemRepositoryData> items);

        Task DeleteAllAsync(DbConnection connection, DbTransaction transaction);
    }
}
