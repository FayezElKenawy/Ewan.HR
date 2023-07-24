using AutoMapper;
using Ewan.HR.Core.Application.Enums.Company;
using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.Global;
using Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAbsent;
using Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcOvertime;
using Ewan.HR.Core.Application.Services.External.BioTimeData.GetData;

namespace Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAttendance
{
    public class CalcAttendanceService : ICalcAttendanceService
    {
        private IBioTimeService _bioTimeService;
        private readonly IMapper _mapper;
        private readonly ICalcOvertime _overTime;
        private readonly ICalcAbsentTime _absentTime;
        public CalcAttendanceService(IBioTimeService getdata, IMapper mapper, ICalcOvertime overTime, ICalcAbsentTime absentTime)
        {
            _bioTimeService = getdata;
            _mapper = mapper;
            _overTime = overTime;
            _absentTime = absentTime;
        }
        /// <summary>
        /// Get Employees Attendance List
        /// </summary>
        /// <param name="employeeList"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="lastId"></param>
        /// <returns></returns>
        public async Task<List<AttendanceDataVM>> GetNewEmployeesAttendanceList(List<GetEmployeeDataVM> employeeList, string start, string end, string lastId)
        {   
            //calc attendance to insert to db
            var employeesNewAttendance = new List<AttendanceDataVM>();

            foreach (var item in employeeList)
            {
                var employeeAttendanceList = await _bioTimeService.GetEmployeeAttendance(item.emp_code, start, end);

                if (employeeAttendanceList.Count() == 0)
                    continue;

                for (int i = 0; i < employeeAttendanceList.Count(); i++)
                {
                    DateTime pDate = DateTime.Parse(employeeAttendanceList.FirstOrDefault().punch_time.Substring(0, 10));
                    var ldata = employeeAttendanceList.Where(c => DateTime.Parse(c.punch_time.Substring(0, 10)).ToString() == pDate.ToString());
                    employeesNewAttendance.Add(_mapper.Map<AttendanceDataVM>(ldata.ToList()));
                    employeeAttendanceList.RemoveRange(0, ldata.Count());
                    i = 0;
                }
            }

            return employeesNewAttendance;
        }

        public async Task<int> CalcAbsentTime(DateTime start, DateTime end, string Dept)
        {
            if (start.Hour == end.Hour)
            {
                return await Task.FromResult(0);
            }
            var absentTime = 0;
            if (Dept == Departments.Saudi.ToString())
            {
                absentTime = await _absentTime.ClacSaudiAbsentTime(start, end);
            }
            else if (Dept == Departments.Muqeem.ToString())
            {
                absentTime = await _absentTime.ClacMuqeemAbsentTime(start, end);
            }
            else if (Dept == Departments.Callcenter.ToString())
            {
                absentTime = await _absentTime.ClacCallcenterAbsentTime(start, end);
            }
            else
            {
                absentTime = await _absentTime.ClacShowroomsAbsentTime(start, end);
            }
            if (absentTime < 0)
            {
                return await Task.FromResult(absentTime * -1);
            }
            return await Task.FromResult(absentTime);
        }

        public async Task<int> CalcOverTime(DateTime start, DateTime end, string Dept)
        {
            if (start.Hour == end.Hour)
            {
                return 0;
            }
            var overTime = 0;
            if (Dept == Departments.Saudi.ToString())
            {
                overTime = await _overTime.ClacSaudiOvertime(start, end);
            }
            else if (Dept == Departments.Callcenter.ToString())
            {
                overTime = await _overTime.ClacCallcenterOvertime(start, end);
            }
            else if (Dept == Departments.Muqeem.ToString())
            {
                overTime = await _overTime.ClacMuqeemOvertime(start, end);
            }
            else
            {
                overTime = await _overTime.ClacShowroomsOvertime(start, end);
            }
            if (overTime < 0)
            {
                return await Task.FromResult(overTime * -1);
            }
            return await Task.FromResult(overTime);
        }

        public Task<int> CalcTotalTime(DateTime start, DateTime end, string Dept)
        {
            if (start.Hour == end.Hour)
            {
                return Task.FromResult(0);
            }
            var da = end.TimeOfDay - start.TimeOfDay;
            return Task.FromResult(da.Hours * 60 + da.Minutes);
        }
    }
}
