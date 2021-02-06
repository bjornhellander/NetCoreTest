using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public class ItemRepositoryService : IItemRepositoryService
    {
        public Task<List<ItemRepositoryData>> GetAllItemsAsync()
        {
            var result = new List<ItemRepositoryData>();
            result.Add(new ItemRepositoryData(1, "Sax"));
            result.Add(new ItemRepositoryData(2, "Kniv"));
            return Task.FromResult(result);
        }
    }
}
