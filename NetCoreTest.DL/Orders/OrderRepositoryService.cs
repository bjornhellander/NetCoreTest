using Microsoft.EntityFrameworkCore;
using NetCoreTest.DL.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Orders
{
    public class OrderRepositoryService : RepositoryServiceBase, IOrderRepositoryService
    {
        public OrderRepositoryService(IInternalTransactionService transactionService)
            : base(transactionService)
        {
        }

        public Task<List<OrderRepositoryData>> GetAllOrdersAsync(Guid transactionId)
        {
            using (var context = GetContext(transactionId))
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

        public Task InitializeAsync()
        {
            using (var context = GetContext())
            {
                context.Database.Migrate();
            }

            return Task.CompletedTask;
        }

        public async Task<List<int>> CreateOrdersAsync(Guid transactionId, List<OrderRepositoryData> orders)
        {
            using (var context = GetContext(transactionId))
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

        public async Task DeleteAllAsync(Guid transactionId)
        {
            using (var context = GetContext(transactionId))
            {
                var entities = context.Orders;
                context.Orders.RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}
