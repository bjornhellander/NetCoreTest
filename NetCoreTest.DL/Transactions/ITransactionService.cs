using System;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Transactions
{
    public interface ITransactionService
    {
        Task<Guid> StartAsync();

        Task CommitAsync(Guid id);

        Task StopAsync(Guid id);
    }
}
