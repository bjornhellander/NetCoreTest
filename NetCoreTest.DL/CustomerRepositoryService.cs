using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public class CustomerRepositoryService : ICustomerRepositoryService
    {
        public Task<List<CustomerRepositoryData>> GetAllCustomersAsync()
        {
            var result = new List<CustomerRepositoryData>();
            result.Add(new CustomerRepositoryData(1, "Kalle"));
            result.Add(new CustomerRepositoryData(2, "Adam"));
            return Task.FromResult(result);
        }
    }
}
