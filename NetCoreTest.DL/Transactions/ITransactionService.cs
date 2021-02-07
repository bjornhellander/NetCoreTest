using System.Threading.Tasks;

namespace NetCoreTest.DL.Transactions
{
    public interface ITransactionService
    {
        Task<Transaction> StartAsync();

        Task CommitAsync(Transaction transaction);

        Task StopAsync(Transaction transaction);
    }
}
