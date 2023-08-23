namespace Ewan.HR.API.Dtos.Attendance
{
    public class MonthSettingsDTO
    {
        public string MonthName { get; set; }
        public int StartDay { get; set; }
        public string StartMonth { get; set; }
        public int EndDay { get; set; }
        public string EndMonth { get; set; }
        public int MonthDays { get; set; }
        public int id { get; set; }
    }
}
