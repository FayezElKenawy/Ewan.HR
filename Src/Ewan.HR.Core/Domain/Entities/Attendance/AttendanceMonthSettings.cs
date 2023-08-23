using SharedCoreLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewan.HR.Core.Domain.Entities.Attendance
{
    public class AttendanceMonthSettings: AuditData
    {
        public string MonthName { get; set; }
        public int StartDay { get; set; }
        public string StartMonth { get; set; }
        public int EndDay { get; set; }
        public string EndMonth { get; set; }
        public int MonthDays { get; set; }
    }
}
