namespace SharedCoreLibrary.Application.Dtos.Request
{
    public class SearchDto
    {
        public List<SearchFieldDto> SearchFields { get; set; } = new List<SearchFieldDto>();
        public string OrderBy { get; set; }
        public string OrderType { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
