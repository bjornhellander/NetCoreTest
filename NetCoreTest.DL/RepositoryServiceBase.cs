using Microsoft.EntityFrameworkCore;
using NetCoreTest.DL.Transactions;
using System;

namespace NetCoreTest.DL
{
    public abstract class RepositoryServiceBase
    {
        private readonly IInternalTransactionService transactionService;

        public RepositoryServiceBase(IInternalTransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [Obsolete("Currently unused")]
        protected DatabaseContext GetContext()
        {
            var result = new DatabaseContext();
            return result;
        }

        protected DatabaseContext GetContext(Guid transactionId)
        {
            var transaction = transactionService.GetTransaction(transactionId);
            var result = new DatabaseContext(transaction.SqlConnection);
            result.Database.UseTransaction(transaction.SqlTransaction);
            return result;
        }
    }
}
