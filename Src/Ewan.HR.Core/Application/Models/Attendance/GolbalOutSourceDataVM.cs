namespace Ewan.HR.Core.Application.Models.Attendance
{
    public class GolbalOutSourceDataVM<T> where T : class
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public string msg { get; set; }
        public int code { get; set; }
        public IList<T> data { get; set; }
    }
}
