using AutoMapper;
using Ewan.HR.Core.Application.Enums.Global;
using Ewan.HR.Core.Application.Models.Request;
using Ewan.HR.Core.Application.Models.Request.Vacation;
using Ewan.HR.Core.Application.Services.Request.MasterData;
using Ewan.HR.Core.Domain.Entities.Vacation;
using Ewan.HR.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace Ewan.HR.Core.Application.Services.Request.Vacation
{
    public class VacationService : IVacationService
    {
        #region Private Members
        private readonly IHRUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;
        #endregion

        #region Constructor
        public VacationService(IHRUnitOfWork unitOfWork, IMapper mapper, IRequestService requestService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestService = requestService;
        }
        #endregion

        #region Implemnetede Functions
        public async Task<IdentityResult> Add(AddVacationVm addVacationVm)
        {

            var requestid = await _requestService.Add(_mapper.Map<AddRequestVM>(addVacationVm));
            if (requestid != null)
            {
                var t = _mapper.Map<VacationRequest>(addVacationVm);
                await _unitOfWork.VacationRepository.AddAsync(t);
                int request = await _unitOfWork.CompleteAsync();
                if (request != 0)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed();
            }
            return IdentityResult.Failed();

        }

        public async Task<VacationDetailsVM> GetById(string RequestId)
        {
            try
            {
                var t = await _unitOfWork.VacationRepository.GetAsync(r => r.RequestId == RequestId);
                return _mapper.Map<VacationDetailsVM>(t);
            }
            catch (Exception ex)
            {
                return new VacationDetailsVM();
            }


        }

        public async Task<IList<VacationDetailsVM>> GetMany(string requestId)
        {
            if (requestId == null)
            {
                return new List<VacationDetailsVM>();
            }
            else
            {
                return _mapper.Map<IList<VacationDetailsVM>>(await _unitOfWork.VacationRepository.GetAsync(r => r.RequestId == requestId));
            }
        }

        public async Task<int> GetMaxId()
        {
            var h = await _unitOfWork.VacationRepository.GetListAsync();
            var d = h.Select(c => int.Parse(c.Number)).Max();
            return int.Parse(d.ToString());
        }

        public async Task<IdentityResult> Update(string ActionFrom, VacationDetailsVM vacationDetailsVM)
        {
            try
            {
                var OldRequest = await GetById(vacationDetailsVM.RequestId);
                JsonPatchDocument<VacationDetailsVM> NewRequest = new();
                var Items = new string[] { };

                if (ActionFrom == ActionFromEnum.Employee.ToString())
                {
                    NewRequest.Replace(t => t.StartDate, vacationDetailsVM.StartDate);
                    NewRequest.Replace(t => t.EndDate, vacationDetailsVM.EndDate);
                    NewRequest.Replace(t => t.ResumptionDate, vacationDetailsVM.ResumptionDate);
                    NewRequest.Replace(t => t.TotalDayes, vacationDetailsVM.TotalDayes);
                    NewRequest.Replace(t => t.ActionFrom, ActionFromEnum.Employee.ToString());
                    Items = new string[] { "StartDate", "EndDate", "ResumptionDate", "TotalDayes" };
                }
                else if (ActionFrom == ActionFromEnum.Supervisor.ToString())
                {
                    NewRequest.Replace(t => t.ReplacingEmployeeNumber, vacationDetailsVM.ReplacingEmployeeNumber);
                    NewRequest.Replace(t => t.ReplacingEmployeeName, vacationDetailsVM.ReplacingEmployeeName);
                    NewRequest.Replace(t => t.WorkHandover, vacationDetailsVM.WorkHandover);
                    NewRequest.Replace(t => t.SupervisorApproval, vacationDetailsVM.SupervisorApproval);
                    NewRequest.Replace(t => t.SDeclineReason, vacationDetailsVM.SDeclineReason);
                    NewRequest.Replace(t => t.ActionFrom, ActionFromEnum.Supervisor.ToString());
                    Items = new string[] { "ReplacingEmployeeNumber", "ReplacingEmployeeName", "WorkHandover" };
                    await _requestService.Update(ActionFrom, _mapper.Map<RequestVM>(vacationDetailsVM));
                }
                else if (ActionFrom == ActionFromEnum.HR.ToString())
                {
                    NewRequest.Replace(t => t.VacationDays, vacationDetailsVM.VacationDays);
                    NewRequest.Replace(t => t.Balance, vacationDetailsVM.Balance);
                    NewRequest.Replace(t => t.RemainingBalance, vacationDetailsVM.RemainingBalance);
                    NewRequest.Replace(t => t.PaidDays, vacationDetailsVM.PaidDays);
                    NewRequest.Replace(t => t.UnpaidDays, vacationDetailsVM.UnpaidDays);
                    NewRequest.Replace(t => t.Holidays, vacationDetailsVM.Holidays);
                    NewRequest.Replace(t => t.InOutVisaFor, vacationDetailsVM.InOutVisaFor);
                    NewRequest.Replace(t => t.TicketsVisaCost, vacationDetailsVM.TicketsVisaCost);
                    NewRequest.Replace(t => t.TicketsFor, vacationDetailsVM.TicketsFor);
                    Items = new string[]
                    { "VacationDays", "Balance", "RemainingBalance", "PaidDays", "UnpaidDays", "Holidays", "InOutVisaFor","TicketsVisaCost","TicketsFor" };
                    await _requestService.Update(ActionFrom, _mapper.Map<RequestVM>(vacationDetailsVM));
                }
                else
                {
                    NewRequest.Replace(t => t.CEOApproval, vacationDetailsVM.CEOApproval);
                    NewRequest.Replace(t => t.CEODeclineReason, vacationDetailsVM.CEODeclineReason);
                    return await _requestService.Update(ActionFrom, _mapper.Map<RequestVM>(vacationDetailsVM));
                }
                NewRequest.ApplyTo(OldRequest);
                var i = _mapper.Map<VacationRequest>(OldRequest);
                var result1 = await _unitOfWork.VacationRepository.CustomPropUpdate(i, Items);
                var result = await _unitOfWork.CompleteAsync();
                if (result != 0)
                {
                    return IdentityResult.Success;
                }
                else
                    return IdentityResult.Failed();
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed();
            }
        }
        #endregion
    }
}


