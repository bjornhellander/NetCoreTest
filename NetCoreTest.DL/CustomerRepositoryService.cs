using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public class CustomerRepositoryService : ICustomerRepositoryService
    {
        public Task<List<CustomerRepositoryData>> GetAllCustomersAsync()
        {
            using (var context = new DatabaseContext())
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
    }
}
