using AutoMapper;
using Ewan.Finance.Core.Application.Services;
using Ewan.HR.API.Dtos.TransactionLogger.Response;
using Ewan.HR.Core.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ewan.HR.API.Controllers.CashBoxController
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = "HRUser")]
    public class TransactionLoggerController : BaseController
    {
        private readonly ITransactionLoggerService service;
        private readonly IMapper mapper;

        public TransactionLoggerController(
            ITransactionLoggerService service,
            IMapper mapper) : base()
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet("GetListPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionLoggerDto>))]
        public IActionResult GetListPage(string sectorType, string module, string entityName, int pageNumber = 0, int pageSize = 10)
        {
            return Ok(mapper.Map<List<TransactionLoggerDto>>(service.GetListPage(sectorType, module, entityName, pageNumber, pageSize)));
        }
    }
}
