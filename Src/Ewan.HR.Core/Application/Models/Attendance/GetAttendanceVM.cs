namespace Ewan.HR.Core.Application.Models.Attendance
{
    public class GetAttendanceVM
    {
        public string id { get; set; }
        public string employee_name { get; set; }
        public string employee_department { get; set; }
        public string emp_code { get; set; }
        public string punch_time { get; set; }
        public string punch_state { get; set; }
        public string terminal_alias { get; set; }
        public string area_alias { get; set; }
    }
}
