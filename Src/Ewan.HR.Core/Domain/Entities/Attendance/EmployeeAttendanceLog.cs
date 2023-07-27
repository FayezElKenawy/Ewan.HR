using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities.Attendance
{
    public class EmployeeAttendanceLog : AuditData
    {
        public string RowId { get; set; }
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
    }
}
