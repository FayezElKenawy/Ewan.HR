namespace SharedCoreLibrary.Domain.Abstractions
{
    public interface IPagedList<TEntity>
        where TEntity : class
    {

        public IPagingData PagingData { get; set; }
        public IEnumerable<TEntity> Entities { get; set; }
    }
}