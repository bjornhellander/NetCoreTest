using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    internal interface IItemRepositoryService
    {
        Task<List<ItemRepositoryData>> GetAllItemsAsync();
    }
}
