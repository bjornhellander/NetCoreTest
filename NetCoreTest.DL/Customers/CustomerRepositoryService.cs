using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Customers
{
    public class CustomerRepositoryService : RepositoryServiceBase, ICustomerRepositoryService
    {
        public Task<List<CustomerRepositoryData>> GetAllCustomersAsync(Transaction transaction)
        {
            using (var context = GetContext(transaction))
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

        public async Task<List<int>> CreateCustomersAsync(Transaction transaction, List<CustomerRepositoryData> customers)
        {
            using (var context = GetContext(transaction))
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

        public async Task DeleteAllAsync(Transaction transaction)
        {
            using (var context = GetContext(transaction))
            {
                var entities = context.Customers;
                context.Customers.RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}
