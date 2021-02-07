using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Orders
{
    public interface IOrderRepositoryService
    {
        Task<List<OrderRepositoryData>> GetAllOrdersAsync(Transaction transaction);

        Task<List<int>> CreateOrdersAsync(Transaction transaction, List<OrderRepositoryData> orders);

        Task DeleteAllAsync(Transaction transaction);
    }
}
