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
        private readonly IGetOutsideDataService _getData;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AttendanceService(IHRUnitOfWork unitOfWork
            , ICalcAttendanceService calcAttendnace
            , IMapper mapper
            , IGetOutsideDataService getData)
        {
            _unitOfWork = unitOfWork;
            _calcAttendnace = calcAttendnace;
            _mapper = mapper;
            _getData = getData;
        }
        #endregion

        #region Functions
        public async Task<string> GetLAstId()
        {//last id inserted to db from bioTime
            try
            {
                var id =(await _unitOfWork.AttendanceRepository.GetPagedListAsync())
                     .Entities
                     .OrderByDescending(c => int.Parse(c.Id))
                     .FirstOrDefault()
                     .Id;
                return  id.ToString();
            }
            catch (Exception)
            {

                return "0";
            }

        }

        public async Task<GlobalReturnVM<AttendanceDataVM>> GetAllAttendanceDataFromBioTime(string start, string end, string[] emps)
        {
            try
            {
                var data = await _getData.GetEmployeeData();
                if (data != null)
                {
                    var attendance = await _calcAttendnace.CalcAttendanceData(data.Details.ToList(), start, end, "0");

                    if (attendance.Message == "Success")
                    {
                        await _unitOfWork.AttendanceRepository.AddRangeAsync(_mapper.Map<IEnumerable<AttendanceData>>(attendance.Details));
                    }
                    var d = await _unitOfWork.CompleteAsync();
                    if (d > 0)
                    {
                        return attendance;
                    }
                }
                return new GlobalReturnVM<AttendanceDataVM>
                {
                    Count = 0,
                    Details = null,
                    Message = "No Data Founded"
                };
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task<GlobalReturnVM<AttendanceDataVM>> GatAttendanceData(string id, string startTime, string endTime)
        {

            if (id != null)
            {
                var attendance1 = _mapper.Map<List<AttendanceDataVM>>(await _unitOfWork.VacationRepository
                                                           .GetAsync(c => c.CreatorId == id
                                                                                    && c.CreationDate == DateTime.Parse(startTime)
                                                                                    && c.CreationDate == DateTime.Parse(endTime)));
                return new GlobalReturnVM<AttendanceDataVM>
                {
                    Count = attendance1.Count(),
                    Details = attendance1,
                    Message = "Success"
                };
            }

            var attendance = _mapper.Map<List<AttendanceDataVM>>(await _unitOfWork
                                                                        .VacationRepository
                                                                        .GetAsync(c => c.CreationDate >= DateTime.Parse(startTime)
                                                                                 && c.CreationDate <= DateTime.Parse(endTime)));

            return new GlobalReturnVM<AttendanceDataVM>
            {

                Count = attendance.Count(),
                Details = attendance,
                Message = "success"
            };
        }
        #endregion
    }
}
