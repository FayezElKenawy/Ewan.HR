namespace SharedCoreLibrary.Domain.Abstractions
{
    public interface IPagingData
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}