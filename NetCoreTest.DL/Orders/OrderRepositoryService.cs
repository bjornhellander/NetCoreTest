using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Orders
{
    public class OrderRepositoryService : RepositoryServiceBase, IOrderRepositoryService
    {
        public Task<List<OrderRepositoryData>> GetAllOrdersAsync(DbConnection connection, DbTransaction transaction)
        {
            using (var context = GetContext(connection, transaction))
            {
                var result = new List<OrderRepositoryData>();

                var entities = context.Orders.OrderBy(x => x.Id).ToList();
                foreach (var entity in entities)
                {
                    result.Add(new OrderRepositoryData(entity.Id, entity.CustomerId, entity.ItemId, entity.Amount));
                }

                return Task.FromResult(result);
            }
        }

        public async Task<List<int>> CreateOrdersAsync(DbConnection connection, DbTransaction transaction, List<OrderRepositoryData> orders)
        {
            using (var context = GetContext(connection, transaction))
            {
                var entities = new List<OrderEntity>();
                foreach (var order in orders)
                {
                    entities.Add(new OrderEntity(0, order.CustomerId, order.ItemId, order.Amount));
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
                var entities = context.Orders;
                context.Orders.RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}
