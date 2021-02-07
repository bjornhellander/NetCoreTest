using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

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

        protected static DatabaseContext GetContext(DbConnection connection, DbTransaction transaction)
        {
            var result = new DatabaseContext(connection);
            result.Database.UseTransaction(transaction);
            return result;
        }
    }
}
