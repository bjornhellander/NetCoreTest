using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public interface ICustomerRepositoryService
    {
        Task<List<CustomerRepositoryData>> GetAllCustomersAsync();
    }
}
