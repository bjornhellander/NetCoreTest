using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public class OrderRepositoryService : IOrderRepositoryService
    {
        public Task<List<OrderRepositoryData>> GetAllOrdersAsync()
        {
            using (var context = new DatabaseContext())
            {
                var result = new List<OrderRepositoryData>();

                var entities = context.Orders.OrderBy(x => x.Id).ToList();
                foreach (var entity in entities)
                {
                    result.Add(new OrderRepositoryData(entity.Id, entity.CustomerId, entity.ItemId, entity.Amount));
                }

                return Task.FromResult(result);
            }
        }
    }
}
