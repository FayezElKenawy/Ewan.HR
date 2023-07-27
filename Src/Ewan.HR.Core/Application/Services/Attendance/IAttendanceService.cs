using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.Global;

namespace Ewan.HR.Core.Application.Services.Attendance
{
    public interface IAttendanceService
    {
        //string GetLAstId();
        Task<bool> GetEmployeesAttendance(string start, string end, string[] emps);
        List<AttendanceDataVM> GatAttendanceData(string id, string startTime, string endTime);
    }
}
