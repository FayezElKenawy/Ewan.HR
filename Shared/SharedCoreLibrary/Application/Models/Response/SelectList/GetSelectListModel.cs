namespace SharedCoreLibrary.Application.Models.Response.SelectList
{
    public class GetSelectListModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class GetSelectListModel<T>
    {
        public T Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
