using AutoMapper;
using Ewan.HR.API.Dtos.PayRoll;
using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Services.PayRoll;
using Microsoft.AspNetCore.Mvc;
using SharedCoreLibrary.Application.Models.Request;
using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.API.Controllers.PayRoll
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PayRollController : BaseController
    {
        private readonly IPayRollService _payRollService;
        private readonly IMapper _mapper;
        public PayRollController(IPayRollService payRollService, IMapper mapper)
        {
            _payRollService = payRollService;
            _mapper = mapper;
        }
        [HttpGet("Calculate")]
        public IActionResult Calculate(string fromDate)
        {
           var t= _payRollService.Calculate(fromDate);
            return Ok(t);
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(SearchModel searchModel)
        {
            var list=_payRollService.GetAll(searchModel);
            return Ok(_mapper.Map<PagedList<PayRollAddDTO>>(list));
        }
        [HttpGet("DownloadPayrollSheet")]
        public async Task<FileResult> DownloadPayrollSheet(string month)
        {
            MemoryStream memoryStream = await _payRollService.PayRollDownload(month);
            return File(memoryStream, "application/vnd.ms-exce", $"Payroll-{DateTime.Now.ToString()}.xlsx");
        }
    }
}
