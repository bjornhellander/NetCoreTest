using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public interface IOrderRepositoryService
    {
        Task<List<OrderRepositoryData>> GetAllOrdersAsync();

        Task<List<int>> CreateOrdersAsync(List<OrderRepositoryData> orders);
    }
}
