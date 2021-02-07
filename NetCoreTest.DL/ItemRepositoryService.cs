using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public class ItemRepositoryService : RepositoryServiceBase, IItemRepositoryService
    {
        public Task<List<ItemRepositoryData>> GetAllItemsAsync(DbConnection connection, DbTransaction transaction)
        {
            using (var context = GetContext(connection, transaction))
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

        public async Task<List<int>> CreateItemsAsync(DbConnection connection, DbTransaction transaction, List<ItemRepositoryData> items)
        {
            using (var context = GetContext(connection, transaction))
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

        public async Task DeleteAllAsync(DbConnection connection, DbTransaction transaction)
        {
            using (var context = GetContext(connection, transaction))
            {
                var entities = context.Items;
                context.Items.RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}
