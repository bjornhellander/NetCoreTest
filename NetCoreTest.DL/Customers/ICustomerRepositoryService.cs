using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Customers
{
    public interface ICustomerRepositoryService
    {
        Task<List<CustomerRepositoryData>> GetAllCustomersAsync(DbConnection connection, DbTransaction transaction);

        Task<List<int>> CreateCustomersAsync(DbConnection connection, DbTransaction transaction, List<CustomerRepositoryData> customers);

        Task DeleteAllAsync(DbConnection connection, DbTransaction transaction);
    }
}
