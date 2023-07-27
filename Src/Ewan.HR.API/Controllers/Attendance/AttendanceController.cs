using AutoMapper;
using Ewan.HR.API.Controllers;
using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Services.Attendance;
using Microsoft.AspNetCore.Mvc;
using SharedCoreLibrary.Application.Models.Request;

namespace Ewan.HR.API.Controllers.Attendance
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceService attendanceService, IMapper mapper)
        {
            _attendanceService = attendanceService;
            _mapper = mapper;
        }

        [HttpPost("GetEmployeesAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult GetEmployeesAttendance(string start, string end, string[] emps)
        {
            var result = _mapper.Map<List<AttendanceDataDTO>>(_attendanceService.GatAttendanceData(null, start, end));
            return Ok(result);
        }
       // [HttpPost("GetEmployeesAttendance1")]
        //public async Task<IActionResult> Getatt()
        //{
        //    var r =await _attendanceService.GetEmployeesAttendance(null,null,null);
        //    return Ok();
        //}
    }
}
