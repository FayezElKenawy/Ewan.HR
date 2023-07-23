using System.Data;

namespace SharedCoreLibrary.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        int Complete();
        Task<int> CompleteAsync();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable);
        void RollBackTransaction();
        void CommitTransaction();
    }

    public interface IUnitOfWork<TContext> : IDisposable 
        where TContext: IDisposable 
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        TContext Context { get; }

        int Complete();
        Task<int> CompleteAsync();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable);
        void RollBackTransaction();
        void CommitTransaction();
    }
}
