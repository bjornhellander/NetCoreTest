using Microsoft.EntityFrameworkCore;
using System;

namespace NetCoreTest.DL
{
    public abstract class RepositoryServiceBase
    {
        [Obsolete("Currently unused")]
        protected static DatabaseContext GetContext()
        {
            var result = new DatabaseContext();
            return result;
        }

        protected static DatabaseContext GetContext(Transaction transaction)
        {
            var result = new DatabaseContext(transaction.SqlConnection);
            result.Database.UseTransaction(transaction.SqlTransaction);
            return result;
        }
    }
}
