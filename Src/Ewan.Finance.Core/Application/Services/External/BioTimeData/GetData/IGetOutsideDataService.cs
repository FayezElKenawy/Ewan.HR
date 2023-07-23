using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.Global;

namespace Ewan.HR.Core.Application.Services.External.BioTimeData.GetData
{
    public interface IGetOutsideDataService
    {
        Task<GlobalReturnVM<GetAttendanceVM>> GetAttendance(string user, string start, string end);
        Task<GlobalReturnVM<GetUsersDataVM>> GetEmployeeData();
    }
}
