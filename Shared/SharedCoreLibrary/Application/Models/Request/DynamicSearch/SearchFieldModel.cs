
namespace SharedCoreLibrary.Application.Models.Request.DynamicSearch
{
    public class SearchFieldModel
    {
        public string FieldName { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public bool IsLocalized { get; set; } = false;
        public string SearchType { get; set; }
    }
}
