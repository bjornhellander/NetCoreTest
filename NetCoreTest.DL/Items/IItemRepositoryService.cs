using NetCoreTest.DL.Transactions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Items
{
    public interface IItemRepositoryService
    {
        Task<List<ItemRepositoryData>> GetAllItemsAsync(Transaction transaction);

        Task<List<int>> CreateItemsAsync(Transaction transaction, List<ItemRepositoryData> items);

        Task DeleteAllAsync(Transaction transaction);
    }
}
