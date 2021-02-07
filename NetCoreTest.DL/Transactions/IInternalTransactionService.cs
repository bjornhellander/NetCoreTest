using System;

namespace NetCoreTest.DL.Transactions
{
    public interface IInternalTransactionService
    {
        InternalTransaction GetTransaction(Guid id);
    }
}
