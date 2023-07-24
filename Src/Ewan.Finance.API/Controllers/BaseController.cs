using Microsoft.AspNetCore.Mvc;
using Ewan.HR.Core.Application.Interfaces.BaseController;

namespace Ewan.HR.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase, IBaseController
    {
        public BaseController()
        {

        }
    }
}
