using Ewan.HR.API.Controllers;
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

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("GetEmployeesAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> GetEmployeesAttendance(string start = null, string end = null, string[] emps = null)
        {
            var result = await _attendanceService.GetEmployeesAttendance(start, end, emps);
            return Ok(result);
        }
    }
}
