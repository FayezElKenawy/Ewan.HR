using Ewan.HR.Core.Application.Models.Request.Vacation;
using Microsoft.AspNetCore.Identity;

namespace Ewan.HR.Core.Application.Services.Request.Vacation
{
    public interface IVacationService
    {
        Task<IdentityResult> Add(AddVacationVm addVacationVm);
        Task<VacationDetailsVM> GetById(string RequestId);
        Task<IdentityResult> Update(string ActionFrom, VacationDetailsVM vacationDetailsVM);
        Task<int> GetMaxId();
        Task<IList<VacationDetailsVM>> GetMany(string requestId);
        //Task<IdentityResult> ReplacementApprove(VacationDetailsVM vacationDetailsVM);

    }
}
