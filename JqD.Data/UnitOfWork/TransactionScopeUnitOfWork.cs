using System;
using System.Transactions;

namespace JqD.Data.UnitOfWork
{
    public class TransactionScopeUnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;

        public TransactionScopeUnitOfWork()
        {
            _transaction = new TransactionScope();
        }

        public void Commit()
        {
            _transaction.Complete();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                //var state = Transaction.Current.TransactionInformation.Status;
                //if (state == TransactionStatus.Active)
                //{
                //    _transaction.Complete();
                //}
                _transaction.Dispose();
                _transaction = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}