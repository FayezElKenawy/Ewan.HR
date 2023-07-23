using Microsoft.AspNetCore.Mvc;
using Ewan.Finance.Core.Application.Interfaces.BaseController;

namespace Ewan.Finance.API.Controllers
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
