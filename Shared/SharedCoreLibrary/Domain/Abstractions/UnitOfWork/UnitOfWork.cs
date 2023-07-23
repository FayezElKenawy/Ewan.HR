using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace SharedCoreLibrary.Domain.Abstractions
{
    public abstract class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext, IDisposable
    {
        readonly TContext _context;
        protected IDbContextTransaction _dbContextTransaction { get; set; }

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        #region Methods
        public abstract IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        public TContext Context => _context;

        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            _dbContextTransaction = Context.Database.BeginTransaction(isolationLevel);
        }

        public virtual void RollBackTransaction()
        {
            _dbContextTransaction.Rollback();
            _dbContextTransaction.Dispose();
        }

        public virtual void CommitTransaction()
        {
            _dbContextTransaction.Commit();
            _dbContextTransaction.Dispose();
        }

        public virtual int Complete()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
          
        }

        public virtual async Task<int> CompleteAsync()
        {
            return await Context.SaveChangesAsync();
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #endregion
    }
}
