using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.Global;

namespace Ewan.HR.Core.Application.Services.External.BioTimeData.GetData
{
    public interface IBioTimeService
    {
        Task<List<GetAttendanceVM>> GetEmployeeAttendance(string user, string start, string end);
        Task<List<GetEmployeeDataVM>> GetEmployeeData();
    }
}
