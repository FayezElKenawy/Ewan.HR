using AutoMapper;
using Ewan.Finance.Core.Application.Services;
using Ewan.HR.API.Dtos.TransactionLogger.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ewan.HR.API.Controllers.CashBoxController
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize(Policy = "HRUser")]
    public class EmployeeController : BaseController
    {
        private readonly ITransactionLoggerService service;
        private readonly IMapper mapper;
        
        public EmployeeController(
            ITransactionLoggerService service,
            IMapper mapper) : base()
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Done");
        }

    }
}
