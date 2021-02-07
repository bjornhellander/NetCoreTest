using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Items
{
    public interface IItemRepositoryService
    {
        Task<List<ItemRepositoryData>> GetAllItemsAsync(Guid transactionId);

        Task<List<int>> CreateItemsAsync(Guid transactionId, List<ItemRepositoryData> items);

        Task DeleteAllAsync(Guid transactionId);
    }
}
