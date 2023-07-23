using Microsoft.AspNetCore.Identity;
using SharedCoreLibrary.Application.Models.Request;
using SharedCoreLibrary.Domain.Entities;
using System.Linq.Expressions;

namespace SharedCoreLibrary.Domain.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        #region Get
        TEntity Get(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        IEnumerable<TEntity> GetList(
            Expression<Func<TEntity, bool>> predicate = null,
            int count = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            int count = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        PagedList<TEntity> GetPagedList(
            Expression<Func<TEntity, bool>> predicate,
            int pageNumber = 0,
            int pageSize = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        Task<PagedList<TEntity>> GetPagedListAsync<TPagedEntity>(
            Expression<Func<TEntity, bool>> predicate,
            int pageNumber = 0,
            int pageSize = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        PagedList<TEntity> GetPagedList(
            Expression<Func<TEntity, bool>> predicate = null,
            SearchModel searchModel = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);


        Task<PagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            SearchModel searchModel = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);
        #endregion

        #region Filters
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Aggregation
        T Max<T>(Func<TEntity, T> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>;
        Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>;
        T Min<T>(Func<TEntity, T> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>;
        Task<T> MinAsync<T>(Expression<Func<TEntity, T>> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>;
        int Count();
        int Count(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);
        Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);
        #endregion

        #region Add
        public TEntity Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(TEntity[] entities);
        void UpdateEntry(object id, object updatedEntity);
        void UpdateEntry(TEntity entity, object updatedEntity);
        Task<IdentityResult> CustomPropUpdate(TEntity entity, string[] Properties);
        #endregion

        #region Remove
        void Remove(object id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void RemoveRange(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
