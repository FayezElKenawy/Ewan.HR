using AutoMapper;
using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAttendance;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig
{
    internal class AttendanceMappingConfig : IMappingAction<IList<GetAttendanceVM>, AttendanceDataVM>
    {
        private readonly ICalcAttendanceService _calc;
        public AttendanceMappingConfig(ICalcAttendanceService calc)
        {
            _calc = calc;
        }
        public void Process(IList<GetAttendanceVM> s, AttendanceDataVM d, ResolutionContext context)
        {
            //var da = DateTime.Parse(s.LastOrDefault().punch_time).TimeOfDay - DateTime.Parse(s.FirstOrDefault().punch_time).TimeOfDay;
            DateTime pDate = DateTime.Parse(s.FirstOrDefault().punch_time.Substring(0, 10));
            d.Id = s.LastOrDefault().id;
            d.EmployeeId = s.LastOrDefault().emp_code;
            d.EmployeeName = s.FirstOrDefault().employee_name;
            d.Date = DateTime.Parse(pDate.ToString());
            d.StartPunchTime = DateTime.Parse(s.FirstOrDefault().punch_time);
            d.EndPunchTime = DateTime.Parse(s.LastOrDefault().punch_time);
            d.ClockIn = DateTime.Parse(s.FirstOrDefault().punch_time);
            d.ClockOut = DateTime.Parse(s.LastOrDefault().punch_time);
            d.Day = new DateTime(pDate.Year, pDate.Month, pDate.Day).DayOfWeek.ToString();
            d.AreaName = s.LastOrDefault().area_alias;
            d.Month = pDate.Month.ToString();
            d.TotalTime = _calc.CalcTotalTime(
                                             DateTime.Parse(s.FirstOrDefault().punch_time),
                                             DateTime.Parse(s.LastOrDefault().punch_time),
                                             s.FirstOrDefault().employee_department)
                                                .Result;

            d.AbsentTime = _calc.CalcAbsentTime(
                                                DateTime.Parse(s.FirstOrDefault().punch_time),
                                                DateTime.Parse(s.LastOrDefault().punch_time),
                                                s.FirstOrDefault().employee_department)
                                                .Result;

            d.OverTime = _calc.CalcOverTime(
                                                DateTime.Parse(s.FirstOrDefault().punch_time),
                                                DateTime.Parse(s.LastOrDefault().punch_time),
                                                s.FirstOrDefault().employee_department)
                                                .Result;

            d.ChangeTime = d.OverTime - d.AbsentTime;

        }
    }
}
