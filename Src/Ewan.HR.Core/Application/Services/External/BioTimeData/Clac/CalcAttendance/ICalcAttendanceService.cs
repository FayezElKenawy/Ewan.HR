using Ewan.HR.Core.Application.Models.Attendance;

namespace Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAttendance
{
    public interface ICalcAttendanceService
    {
        Task<List<AttendanceDataVM>> GetNewEmployeesAttendanceList(List<GetEmployeeDataVM> usersCode, string start, string end, string lastId);
        Task<int> CalcAbsentTime(DateTime start, DateTime end, string Dept);
        Task<int> CalcOverTime(DateTime start, DateTime end, string Dept);
        Task<int> CalcTotalTime(DateTime start, DateTime end, string Dept);
    }
}
