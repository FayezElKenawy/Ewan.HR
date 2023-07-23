using Ewan.Finance.API.Controllers;
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

        [HttpPost("GetPagedList")]
        public async Task<IActionResult> GetPagedList(SearchModel searchModel)
        {
            var t =await _attendanceService.GetAllAttendanceDataFromBioTime(null, null, null);
            return Ok(t.Details);
        }
    }
}
