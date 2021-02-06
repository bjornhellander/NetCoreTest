using System;
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

        public async Task<List<int>> CreateItemsAsync(List<ItemRepositoryData> items)
        {
            using (var context = new DatabaseContext())
            {
                var entities = new List<ItemEntity>();
                foreach (var item in items)
                {
                    entities.Add(new ItemEntity(0, item.Name));
                }

                context.AddRange(entities);
                await context.SaveChangesAsync();

                var ids = entities.Select(x => x.Id).ToList();
                return ids;
            }
        }

        public async Task DeleteAllAsync()
        {
            using (var context = new DatabaseContext())
            {
                var entities = context.Items;
                context.Items.RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}
