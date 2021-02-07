using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace NetCoreTest.DL
{
    public interface IOrderRepositoryService
    {
        Task<List<OrderRepositoryData>> GetAllOrdersAsync(DbConnection connection, DbTransaction transaction);

        Task<List<int>> CreateOrdersAsync(DbConnection connection, DbTransaction transaction, List<OrderRepositoryData> orders);

        Task DeleteAllAsync(DbConnection connection, DbTransaction transaction);
    }
}
