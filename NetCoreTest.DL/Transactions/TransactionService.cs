using System.Threading.Tasks;

namespace NetCoreTest.DL.Transactions
{
    public class TransactionService : ITransactionService
    {
        public Task<Transaction> StartAsync()
        {
            var transaction = new Transaction();
            return Task.FromResult(transaction);
        }

        public Task CommitAsync(Transaction transaction)
        {
            transaction.Commit();
            return Task.CompletedTask;
        }

        public Task StopAsync(Transaction transaction)
        {
            transaction.Dispose();
            return Task.CompletedTask;
        }
    }
}
