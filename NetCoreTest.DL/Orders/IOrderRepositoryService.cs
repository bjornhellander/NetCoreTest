using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Orders
{
    public interface IOrderRepositoryService
    {
        Task<List<OrderRepositoryData>> GetAllOrdersAsync(Guid transactionId);

        Task<List<int>> CreateOrdersAsync(Guid transactionId, List<OrderRepositoryData> orders);

        Task DeleteAllAsync(Guid transactionId);
    }
}
