using AutoMapper;
using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.Global;
using Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAttendance;
using Ewan.HR.Core.Application.Services.External.BioTimeData.GetData;
using Ewan.HR.Core.Domain.Entities.Attendance;
using Ewan.HR.Core.Domain.Interfaces;

namespace Ewan.HR.Core.Application.Services.Attendance
{
    public class AttendanceService : IAttendanceService
    {
        #region Private Members
        private readonly IHRUnitOfWork _unitOfWork;
        private readonly ICalcAttendanceService _calcAttendnace;
        private readonly IBioTimeService _bioTimeService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AttendanceService(IHRUnitOfWork unitOfWork
            , ICalcAttendanceService calcAttendnace
            , IMapper mapper
            , IBioTimeService bioTimeService)
        {
            _unitOfWork = unitOfWork;
            _calcAttendnace = calcAttendnace;
            _mapper = mapper;
            _bioTimeService = bioTimeService;
        }
        #endregion

        #region Functions
        public string GetLAstId()
        {
            var lastAttendanceRow = _unitOfWork.AttendanceRepository.GetList()
                     .OrderByDescending(c => int.Parse(c.Id))
                     .FirstOrDefault();

            if (lastAttendanceRow != null)
                return lastAttendanceRow.Id.ToString();
            return string.Empty;
        }

        /// <summary>
        /// 1-Get all employees
        /// 2-Calculate attendance for every employee after last insersion
        /// 3-insert new attendance in db
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="emps"></param>
        /// <returns></returns>
        public async Task<bool> GetEmployeesAttendance(string start, string end, string[] emps)
        {
            var employeeList = await _bioTimeService.GetEmployeeData();
            if (employeeList != null)
            {
                var employeesNewAttendance = await _calcAttendnace.GetNewEmployeesAttendanceList(employeeList, start, end, "0");

                if (employeesNewAttendance != null)
                    await _unitOfWork.AttendanceRepository.AddRangeAsync( _mapper.Map<IEnumerable<EmployeeAttendanceLog>>(employeesNewAttendance));

                var saveResult = await _unitOfWork.CompleteAsync();
                if (saveResult <= 0)
                    return false;

                return true;
            }

            return false;
        }

        public async Task<List<AttendanceDataVM>> GatAttendanceData(string id, string startTime, string endTime)
        {

            if (id != null)
            {
                var attendance1 = _mapper.Map<List<AttendanceDataVM>>(await _unitOfWork.VacationRepository
                                                           .GetAsync(c => c.CreatorId == id
                                                                                    && c.CreationDate == DateTime.Parse(startTime)
                                                                                    && c.CreationDate == DateTime.Parse(endTime)));
                return attendance1;
            }

            var attendance = _mapper.Map<List<AttendanceDataVM>>(await _unitOfWork
                                                                        .VacationRepository
                                                                        .GetAsync(c => c.CreationDate >= DateTime.Parse(startTime)
                                                                                 && c.CreationDate <= DateTime.Parse(endTime)));

            return attendance;
        }
        #endregion
    }
}
