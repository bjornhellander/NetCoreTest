using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public interface IItemRepositoryService
    {
        Task<List<ItemRepositoryData>> GetAllItemsAsync();

        Task<List<int>> CreateItemsAsync(List<ItemRepositoryData> items);
    }
}
