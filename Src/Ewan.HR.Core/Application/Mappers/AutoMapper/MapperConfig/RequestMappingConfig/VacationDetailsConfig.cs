using AutoMapper;
using Ewan.HR.Core.Application.Models.Request.Vacation;
using Ewan.HR.Core.Application.Services.Request.MasterData;
using Ewan.HR.Core.Domain.Entities.Vacation;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig.RequestMappingConfig
{
    internal class VacationDetailsConfig : IMappingAction<VacationRequest, VacationDetailsVM>
    {
        private readonly IRequestService _requestService;

        public VacationDetailsConfig(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public void Process(VacationRequest source, VacationDetailsVM destination, ResolutionContext context)
        {
            var request = _requestService.GetById(source.RequestId).Result;
            destination.RequestId = request.RequestId;
            destination.SupervisorApprovalStatus = request.SupervisorApprovalStatus;
            destination.CEOApproval = request.CEOApproval;
            destination.CEOApprovalDate = request.CEOApprovalDate;
            destination.CEODeclineReason = request.CEODeclineReason;
            destination.SendtoCEO = request.SendtoCEO;
            destination.CEOApprovalStatus = request.CEOApprovalStatus;
            destination.SDeclineReason = request.SDeclineReason;
            destination.SendtoSupervisor = request.SendtoSupervisor;
            destination.Status = request.Status;
            destination.SupervisorApproval = request.SupervisorApproval;
            destination.SupervisorApprovalDate = request.SupervisorApprovalDate;
            destination.SupervisorId = request.SupervisorId;
            destination.SupervisorName = request.SupervisorName;
            destination.CEOId = request.CEOId;
            destination.CEOName = request.CEOName;
            destination.EmployeeName = request.EmployeeName;
            destination.EmployeeNumber = request.EmployeeNumber;
            destination.EmployeeSignature = request.EmployeeSignature;
            destination.Department = request.Department;


        }
    }
}
