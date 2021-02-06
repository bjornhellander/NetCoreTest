using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public class OrderRepositoryService : IOrderRepositoryService
    {
        public Task<List<OrderRepositoryData>> GetAllOrdersAsync()
        {
            var result = new List<OrderRepositoryData>();
            result.Add(new OrderRepositoryData(1, 1, 1, 11));
            result.Add(new OrderRepositoryData(2, 2, 1, 22));
            result.Add(new OrderRepositoryData(3, 1, 2, 33));
            return Task.FromResult(result);
        }
    }
}
