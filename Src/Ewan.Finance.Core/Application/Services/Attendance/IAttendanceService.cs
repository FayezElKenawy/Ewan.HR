using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.Global;

namespace Ewan.HR.Core.Application.Services.Attendance
{
    public interface IAttendanceService
    {
        Task<string> GetLAstId();
        Task<GlobalReturnVM<AttendanceDataVM>> GetAllAttendanceDataFromBioTime(string start, string end, string[] emps);
        Task<GlobalReturnVM<AttendanceDataVM>> GatAttendanceData(string id, string startTime, string endTime);
    }
}
