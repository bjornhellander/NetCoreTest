using NetCoreTest.DL.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Items
{
    public class ItemRepositoryService : RepositoryServiceBase, IItemRepositoryService
    {
        public ItemRepositoryService(IInternalTransactionService transactionService)
            : base(transactionService)
        {
        }

        public Task<List<ItemRepositoryData>> GetAllItemsAsync(Guid transactionId)
        {
            using (var context = GetContext(transactionId))
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

        public async Task<List<int>> CreateItemsAsync(Guid transactionId, List<ItemRepositoryData> items)
        {
            using (var context = GetContext(transactionId))
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

        public async Task DeleteAllAsync(Guid transactionId)
        {
            using (var context = GetContext(transactionId))
            {
                var entities = context.Items;
                context.Items.RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}
