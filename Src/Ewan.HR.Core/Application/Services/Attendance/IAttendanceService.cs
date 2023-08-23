using Ewan.HR.Core.Application.Models.Attendance;
using SharedCoreLibrary.Application.Models.Request;
using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Application.Services.Attendance
{
    public interface IAttendanceService
    {
        //string GetLAstId();
        Task<bool> AddEmployeesAttendance(string start, string end, string[] emps);
        List<AttendanceDataVM> GatAttendanceData(string id, string startTime, string endTime);
        Task<PagedList<AttendanceDataVM>> AttendanceDataPagedList(SearchModel searchModel);
        List<AttendanceDataVM> GetAttendnaceList();
        List<AttendanceDataVM> GetAttendnaceListByMonth(string month,string year);
        Task<MemoryStream> DownloadAttendnace(string id, string startTime, string endTime);
        Task<GetMonthSettingsVM> GetMonths();
        MonthSettingsVM GetMonthSettings(string month);
        #region Post
        Task<GetMonthSettingsVM> InsertSettings(int from,int to);
        #endregion
    }
}
