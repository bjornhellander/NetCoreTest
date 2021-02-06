using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public interface ICustomerRepositoryService
    {
        Task<List<CustomerRepositoryData>> GetAllCustomersAsync();

        Task<List<int>> CreateCustomersAsync(List<CustomerRepositoryData> customers);

        Task DeleteAllAsync();
    }
}
