using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Customers
{
    public interface ICustomerRepositoryService
    {
        Task<List<CustomerRepositoryData>> GetAllCustomersAsync(Transaction transaction);

        Task<List<int>> CreateCustomersAsync(Transaction transaction, List<CustomerRepositoryData> customers);

        Task DeleteAllAsync(Transaction transaction);
    }
}
