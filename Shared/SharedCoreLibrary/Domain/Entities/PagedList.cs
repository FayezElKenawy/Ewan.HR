
using SharedCoreLibrary.Domain.Abstractions;

namespace SharedCoreLibrary.Domain.Entities
{
    public class PagedList<TEntity> : IPagedList<TEntity>
        where TEntity : class
    {
        public IPagingData PagingData { get; set; }
        public IEnumerable<TEntity> Entities { get; set; }
    }
}