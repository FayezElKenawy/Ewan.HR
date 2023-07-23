namespace SharedCoreLibrary.Application.Dtos.Response
{
    public class GetSelectListDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
       public string FullName { get; set; }
    }

    public class GetSelectListDto<T>
    {
        public T Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
