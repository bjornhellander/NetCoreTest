using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace NetCoreTest.DL.Transactions
{
    public class TransactionService : ITransactionService, IInternalTransactionService
    {
        private readonly ConcurrentDictionary<Guid, InternalTransaction> transactions = new ConcurrentDictionary<Guid, InternalTransaction>();

        public Task<Guid> StartAsync()
        {
            var id = Guid.NewGuid();
            var transaction = new InternalTransaction();

            if (transactions.TryAdd(id, transaction))
            {
                return Task.FromResult(id);
            }
            else
            {
                throw new InvalidOperationException("Transaction already existed");
            }
        }

        public Task CommitAsync(Guid id)
        {
            if (transactions.TryGetValue(id, out var transaction))
            {
                transaction.Commit();
                return Task.CompletedTask;
            }
            else
            {
                throw new InvalidOperationException("Transaction did not exist");
            }
        }

        public Task StopAsync(Guid id)
        {
            if (transactions.TryRemove(id, out var transaction))
            {
                transaction.Dispose();
                return Task.CompletedTask;
            }
            else
            {
                throw new InvalidOperationException("Transaction did not exist");
            }
        }

        public InternalTransaction GetTransaction(Guid id)
        {
            if (transactions.TryGetValue(id, out var transaction))
            {
                return transaction;
            }
            else
            {
                throw new InvalidOperationException("Transaction did not exist");
            }
        }
    }
}
