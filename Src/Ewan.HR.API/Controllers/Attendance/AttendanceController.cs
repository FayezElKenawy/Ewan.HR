using AutoMapper;
using Ewan.HR.API.Dtos.Attendance;
using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Services.Attendance;
using Ewan.HR.Core.Application.Services.PayRoll;
using Microsoft.AspNetCore.Mvc;
using SharedCoreLibrary.Application.Models.Request;
using SharedCoreLibrary.Application.Models.Request.DynamicSearch;
using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.API.Controllers.Attendance
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _mapper;
        private readonly IPayRollService _payRoll;

        public AttendanceController(IAttendanceService attendanceService, IMapper mapper, IPayRollService payRoll)
        {
            _attendanceService = attendanceService;
            _mapper = mapper;
            _payRoll = payRoll;
        }

        [HttpPost("GetEmployeesAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AttendanceDataDTO))]
        public IActionResult GetEmployeesAttendance(string start, string end, string[] emps)
        {
            var result = _mapper.Map<List<AttendanceDataDTO>>(_attendanceService.GatAttendanceData(null, start, end));
            return Ok(result);
        }

        [HttpPost("GetAllAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AttendanceDataDTO))]
        public async Task<IActionResult> GetAttendance(SearchModel searchModel)
        {
            var result = _mapper.Map<PagedList<AttendanceDataDTO>>(await _attendanceService.AttendanceDataPagedList(searchModel));
            return Ok(result);
        }

        [HttpGet("DownloadAttendnace")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AttendanceDataDTO))]
        public async Task<FileResult> DownloadAttendnace(string id, string start, string end)
        {
            var s = DateTime.Now;
            var e = DateTime.Now;
            try
            {
                s = DateTime.Parse(start);
                e = DateTime.Parse(end);
            }
            catch (Exception ex)
            {

                s = DateTime.Parse(start.Substring(4, 11));
                e = DateTime.Parse(end.Substring(4, 11));
            }

            MemoryStream memoryStream = await _attendanceService.DownloadAttendnace(id, s.ToString("yyyy-MM-dd"), e.ToString("yyyy-MM-dd"));
            return File(memoryStream, "application/vnd.ms-exce", $"Attendance-{DateTime.Now.ToString()}.xlsx");
        }

        [HttpGet("GetMonthSettings")]
        public async Task<IActionResult> GetMonthSettings()
        {
            return Ok(_mapper.Map<GetMonthSettingsDTO>(await _attendanceService.GetMonths()));
        }
        [HttpPost("InsertSettings")]
        public async Task<IActionResult> InsertSettings(GetMonthSettingsDTO month)
        {
            return Ok(_mapper.Map<GetMonthSettingsDTO>(
                await _attendanceService.InsertSettings(int.Parse(month.from),int.Parse(month.to))));
        }

    }
}
