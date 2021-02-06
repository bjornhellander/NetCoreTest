using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public class ItemRepositoryService : IItemRepositoryService
    {
        public Task<List<ItemRepositoryData>> GetAllItemsAsync()
        {
            using (var context = new DatabaseContext())
            {
                var result = new List<ItemRepositoryData>();

                var entities = context.Items.OrderBy(x => x.Id).ToList();
                foreach (var entity in entities)
                {
                    result.Add(new ItemRepositoryData(entity.Id, entity.Name));
                }

                return Task.FromResult(result);
            }
        }
    }
}
