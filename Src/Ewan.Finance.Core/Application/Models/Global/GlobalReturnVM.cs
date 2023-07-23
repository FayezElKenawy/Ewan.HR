namespace Ewan.HR.Core.Application.Models.Global
{
    public class GlobalReturnVM<T> where T : class
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public IList<T> Details { get; set; }
    }
}
