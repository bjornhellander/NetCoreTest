using NetCoreTest.DL.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Customers
{
    public class CustomerRepositoryService : RepositoryServiceBase, ICustomerRepositoryService
    {
        public CustomerRepositoryService(IInternalTransactionService transactionService)
            : base(transactionService)
        {
        }

        public Task<List<CustomerRepositoryData>> GetAllCustomersAsync(Guid transactionId)
        {
            using (var context = GetContext(transactionId))
            {
                var result = new List<CustomerRepositoryData>();

                var entities = context.Customers.OrderBy(x => x.Id).ToList();
                foreach (var entity in entities)
                {
                    result.Add(new CustomerRepositoryData(entity.Id, entity.Name));
                }

                return Task.FromResult(result);
            }
        }

        public async Task<List<int>> CreateCustomersAsync(Guid transactionId, List<CustomerRepositoryData> customers)
        {
            using (var context = GetContext(transactionId))
            {
                var entities = new List<CustomerEntity>();
                foreach (var customer in customers)
                {
                    entities.Add(new CustomerEntity(0, customer.Name));
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
                var entities = context.Customers;
                context.Customers.RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}
