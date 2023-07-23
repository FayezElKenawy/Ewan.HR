using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.Global;

namespace Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAttendance
{
    public interface ICalcAttendanceService
    {
        Task<GlobalReturnVM<AttendanceDataVM>> CalcAttendanceData(List<GetUsersDataVM> usersCode, string start, string end, string lastId);
        Task<int> CalcAbsentTime(DateTime start, DateTime end, string Dept);
        Task<int> CalcOverTime(DateTime start, DateTime end, string Dept);
        Task<int> CalcTotalTime(DateTime start, DateTime end, string Dept);
    }
}
