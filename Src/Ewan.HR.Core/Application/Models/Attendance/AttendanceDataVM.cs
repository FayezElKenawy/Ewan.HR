namespace Ewan.HR.Core.Application.Models.Attendance
{
    public class AttendanceDataVM
    {
        public string Nationality { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public DateTime Date { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
        public int TotalTime { get; set; }
        public int AbsentTime { get; set; }
        public int OverTime { get; set; }
        public int ChangeTime { get; set; }
        public DateTime StartPunchTime { get; set; }
        public DateTime EndPunchTime { get; set; }
        public byte IsAttendance { get; set; }
        public string AreaName { get; set; }
    }
}
