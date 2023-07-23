namespace SharedCoreLibrary.Application.Dtos.Request
{
    public class SearchFieldDto
    {
        public string FieldName { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public string DateSearchType { get; set; }
    }
}
