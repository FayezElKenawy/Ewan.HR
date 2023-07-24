using AutoMapper;
using Ewan.HR.Core.Application.Enums.Global;
using Ewan.HR.Core.Application.Enums.Request;
using Ewan.HR.Core.Application.Models.Request;
using Ewan.HR.Core.Application.Services.Employee;
using Ewan.HR.Core.Domain.Entities.Request.MasterData;
using Ewan.HR.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace Ewan.HR.Core.Application.Services.Request.MasterData
{
    public class RequestService : IRequestService
    {
        #region Private Fields
        private readonly IHRUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Public Fields
        public IList<TypesVM> Types => _mapper.Map<IList<TypesVM>>(_unitOfWork.RequestTypeRepository.GetListAsync().Result);
        #endregion

        #region Constructor
        public RequestService(IHRUnitOfWork unitOfWork, IMapper mapper, IEmployeeService employeeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _employeeService = employeeService;
        }
        #endregion

        #region Implemented Functions

        #region Get Functions
        public async Task<IList<RequestVM>> GetByEmployeeId(string EmployeeId)
        {
            var f = await _unitOfWork.RequestRepository.GetListAsync(e => e.CreatorId == EmployeeId);
            return _mapper.Map<IList<RequestVM>>(f);
        }

        public AddRequestVM GetEmployeeData(string EmployeeId)
        {
            var employeeVM = _employeeService.SelectCustom(EmployeeId).Result;
            if (employeeVM.EmployeeId != null)
            {
                return _mapper.Map<AddRequestVM>(employeeVM);
            }
            return new AddRequestVM() { };
        }

        public async Task<IList<RequestVM>> GetByManagerId(string ManagerId)
        {
            try
            {
                var mm = _employeeService.ReturnEmployeeNumber(ManagerId);
                if (ManagerId != mm)
                {
                    var t = await _unitOfWork.RequestRepository
                                                            .GetListAsync(e => e.SupervisorId == ManagerId 
                                                                            || e.SupervisorId == _employeeService.ReturnEmployeeNumber(ManagerId));
                    return _mapper.Map<IList<RequestVM>>(t);
                }

                var t1 = await _unitOfWork.RequestRepository.GetListAsync(e => e.SupervisorId == ManagerId);
                return _mapper.Map<IList<RequestVM>>(t1);
            }
            catch (Exception ex)
            {
                return new List<RequestVM>();
            }

        }

        public async Task<IList<RequestVM>> GetByCeoId(string CeoId)
        {
            var t = await _unitOfWork.RequestRepository.GetListAsync(e => e.CEOId == CeoId);
            return _mapper.Map<IList<RequestVM>>(t);
        }

        public async Task<RequestVM> GetById(string RequestId)
        {
            return _mapper.Map<RequestVM>(await _unitOfWork.RequestRepository.GetAsync(c=>c.RequestId==RequestId));
        }

        public async Task<IList<RequestVM>> GetAllToHr()
        {
            var t = await _unitOfWork.RequestRepository.GetPagedListAsync();
            return _mapper.Map<IList<RequestVM>>(t.Entities.Where(c => c.SendToHr != null));
        }
        #endregion

        #region Post Function
        public async Task<string> Add(AddRequestVM requestVM)
        {
            try
            {
                var t = _mapper.Map<RequestMasterData>(requestVM);
                string requestid = _unitOfWork.RequestRepository.Add(t).RequestId;
                await _unitOfWork.CompleteAsync();
                return requestid;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public async Task<IdentityResult> Update(string actionFrom, RequestVM request)
        {
            try
            {
                var OldRequest = await _unitOfWork.RequestRepository.GetAsync(c=>c.RequestId==request.RequestId);
                JsonPatchDocument<RequestMasterData> NewRequest = new();
                if ((request.SendtoCEO == 0 || string.IsNullOrEmpty(request.SendtoCEO.ToString())) && actionFrom != ActionFromEnum.Ceo.ToString())
                {
                    NewRequest.Replace(t => t.SupervisorApproval, request.SupervisorApproval);
                    NewRequest.Replace(t => t.SupervisorApprovalDate, DateTime.Now);
                    NewRequest.Replace(t => t.SendtoSupervisor, byte.Parse("2"));
                    if (actionFrom == ActionFromEnum.Supervisor.ToString())//action from superviso
                    {
                        if (request.SupervisorApproval == "0")//Approved
                        {

                            NewRequest.Replace(t => t.SendToHr, byte.Parse("1"));
                            NewRequest.Replace(t => t.SendToHrDat, DateTime.Now);
                            NewRequest.Replace(t => t.SupervisorApprovalStatus, RequestStatus.DMApprove);
                            NewRequest.Replace(t => t.SendToHrStatus, RequestStatus.SendToCEO + DateTime.Now);
                            NewRequest.ApplyTo(OldRequest);
                            await _unitOfWork.RequestRepository.CustomPropUpdate(OldRequest,
                                new string[] { "SupervisorApproval", "SupervisorApprovalDate", "SendtoSupervisor",
                                           "SendToHr", "SendToHrDat", "SupervisorApprovalStatus", "SendToHrStatus" });
                        }
                        else if (request.SupervisorApproval == "4")//Rejected
                        {
                            NewRequest.Replace(t => t.SupervisorDeclineReason, request.SDeclineReason);
                            NewRequest.Replace(t => t.SupervisorApprovalStatus, RequestStatus.DMReject);
                            NewRequest.Replace(t => t.ApprovalStatus, byte.Parse("1"));
                            NewRequest.Replace(t => t.Status, byte.Parse("1"));
                            NewRequest.ApplyTo(OldRequest);
                            await _unitOfWork.RequestRepository.CustomPropUpdate(OldRequest,
                                new string[] { "SupervisorApproval", "SupervisorApprovalDate","SendtoSupervisor", "SupervisorDeclineReason",
                                        "SupervisorApprovalStatus",   "ApprovalStatus", "Status" });
                        }
                    }
                    else if (actionFrom == ActionFromEnum.HR.ToString())//action from hr department
                    {
                        NewRequest.Replace(t => t.HrApproval, byte.Parse("1"));
                        NewRequest.Replace(t => t.HrApprovalDate, DateTime.Now);
                        NewRequest.Replace(t => t.SendtoCEO, byte.Parse("1"));
                        NewRequest.Replace(t => t.CEOSentDate, DateTime.Now);
                        NewRequest.Replace(t => t.CEOApprovalStatus, RequestStatus.SendToCEO);
                        NewRequest.Replace(t => t.CEOId, _employeeService.ReturnCeo().EmployeeId.ToString());
                        NewRequest.ApplyTo(OldRequest);
                        await _unitOfWork.RequestRepository.CustomPropUpdate(OldRequest,
                            new string[] { "HrApproval", "HrApprovalDate", "SendtoCEO",
                                           "CEOSentDate", "CEOApprovalStatus", "CEOId" });
                    }

                }
                else if (request.SendtoCEO == 1 || actionFrom == ActionFromEnum.Ceo.ToString())
                {
                    NewRequest.Replace(t => t.CEOApproval, request.CEOApproval);
                    NewRequest.Replace(t => t.CEOApprovalDate, DateTime.Now);
                    NewRequest.Replace(t => t.Status, byte.Parse("1"));
                    if (request.CEOApproval == "0")//Approved
                    {
                        NewRequest.Replace(t => t.SendtoCEO, byte.Parse("2"));
                        NewRequest.Replace(t => t.CEOApprovalStatus, RequestStatus.CEOApprove);
                        NewRequest.Replace(t => t.ApprovalStatus, byte.Parse("2"));
                        NewRequest.ApplyTo(OldRequest);
                        await _unitOfWork.RequestRepository.CustomPropUpdate(OldRequest,
                                 new string[] { "CEOApproval", "CEOApprovalDate", "SendtoCEO",
                                           "CEOApprovalStatus","Status","ApprovalStatus"});
                    }
                    else if (request.CEOApproval == "4")//rejected
                    {
                        NewRequest.Replace(t => t.SendtoCEO, byte.Parse("2"));
                        NewRequest.Replace(t => t.CEODeclineReason, request.CEODeclineReason);
                        NewRequest.Replace(t => t.CEOApprovalStatus, RequestStatus.CEOReject);
                        NewRequest.Replace(t => t.ApprovalStatus, byte.Parse("1"));
                        NewRequest.ApplyTo(OldRequest);
                        await _unitOfWork.RequestRepository.CustomPropUpdate(OldRequest,
                                 new string[] { "CEOApproval", "CEOApprovalDate", "SendtoCEO","CEOApprovalStatus","ApprovalStatus",
                                           "CEODeclineReason","Status"});
                    }
                }
                var result = await _unitOfWork.CompleteAsync();
                if (result != 0)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed();
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed();
            }
        }
        #endregion

        #endregion
    }
}
