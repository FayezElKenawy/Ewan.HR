using Ewan.HR.Core.Application.Models.Request;
using Microsoft.AspNetCore.Identity;

namespace Ewan.HR.Core.Application.Services.Request.MasterData
{
    public interface IRequestService
    {
        IList<TypesVM> Types { get; }
        Task<string> Add(AddRequestVM requestVM);
        AddRequestVM GetEmployeeData(string EmployeeId);
        Task<IList<RequestVM>> GetByManagerId(string ManagerId);
        Task<IList<RequestVM>> GetByCeoId(string CeoId);
        Task<IList<RequestVM>> GetByEmployeeId(string EmployeeId);
        Task<RequestVM> GetById(string RequestId);
        Task<IdentityResult> Update(string actionFrom, RequestVM requestVM);
        Task<IList<RequestVM>> GetAllToHr();
    }
}
