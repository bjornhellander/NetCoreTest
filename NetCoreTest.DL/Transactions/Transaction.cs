using Microsoft.Data.SqlClient;
using System;

namespace NetCoreTest.DL.Transactions
{
    public class InternalTransaction : IDisposable
    {
        public readonly SqlConnection SqlConnection;
        public readonly SqlTransaction SqlTransaction;
        private bool isDisposed = false;

        public InternalTransaction()
        {
            SqlConnection = new SqlConnection(DatabaseContext.ConnectionString);
            SqlConnection.Open();

            SqlTransaction = SqlConnection.BeginTransaction();
        }

        public void Commit()
        {
            if (isDisposed)
            {
                throw new InvalidOperationException("Transaction is already disposed");
            }

            SqlTransaction.Commit();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    SqlTransaction.Dispose();
                    SqlConnection.Dispose();
                }

                isDisposed = true;
            }
        }
    }
}
