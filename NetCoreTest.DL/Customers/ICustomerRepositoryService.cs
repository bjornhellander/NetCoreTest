using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Customers
{
    public interface ICustomerRepositoryService
    {
        Task<List<CustomerRepositoryData>> GetAllCustomersAsync(Guid transactionId);

        Task<List<int>> CreateCustomersAsync(Guid transactionId, List<CustomerRepositoryData> customers);

        Task DeleteAllAsync(Guid transactionId);
    }
}
