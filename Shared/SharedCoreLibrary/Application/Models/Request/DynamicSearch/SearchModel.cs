using SharedCoreLibrary.Application.Models.Request.DynamicSearch;

namespace SharedCoreLibrary.Application.Models.Request
{
    public class SearchModel
    {
        public List<SearchFieldModel> SearchFields { get; set; } = new List<SearchFieldModel>();
        public string OrderBy { get; set; }
        public string OrderType { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
