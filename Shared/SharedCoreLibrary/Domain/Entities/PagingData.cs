
using SharedCoreLibrary.Domain.Abstractions;

namespace SharedCoreLibrary.Domain.Entities
{
    public class PagingData : IPagingData
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}