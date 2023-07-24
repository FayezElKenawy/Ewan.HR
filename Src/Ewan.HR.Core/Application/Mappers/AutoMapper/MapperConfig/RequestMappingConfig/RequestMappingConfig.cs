using AutoMapper;
using Ewan.HR.Core.Application.Enums.Request;
using Ewan.HR.Core.Application.Models.Request;
using Ewan.HR.Core.Application.Services.Employee;
using Ewan.HR.Core.Application.Services.Request.MasterData;
using Ewan.HR.Core.Domain.Entities.Request.MasterData;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig.RequestMappingConfig
{
    public class RequestMappingConfig : IMappingAction<RequestMasterData, RequestVM>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRequestService _requestService;

        public RequestMappingConfig(IEmployeeService employeeService, IRequestService requestService)
        {
            _employeeService = employeeService;
            _requestService = requestService;
        }
        public void Process(RequestMasterData source, RequestVM destination, ResolutionContext context)
        {
            destination.DateCreated = source.CreationDate.ToString();
            //var Supervisor = _employeeService.Find(source.SupervisorId);
            if (source.CEOId != null)
            {
                var CEO = _employeeService.Find(source.CEOId);
                destination.CEOName = CEO.FristName + ' ' + CEO.LastName;
            }
            destination.Type = _requestService.Types.Where(t => t.TypeId == source.TypeId).FirstOrDefault().TypeName;
            if (source.SupervisorId != null)
            {
                var supervisor = _employeeService.RetuernDirectmanager(source.EmployeeNumber).Result;
                destination.SupervisorName = supervisor.FristName + ' ' + supervisor.LastName;
            }


            #region Approval Status Options
            if (source.ApprovalStatus == 1)
            {
                destination.LastApproval = ApprovalStatus.Rejected;
            }
            else if (source.ApprovalStatus.ToString() == "0")
            {
                destination.LastApproval = ApprovalStatus.Pending;
            }
            else
                destination.LastApproval = ApprovalStatus.Approved;
            #endregion

            #region  Status Options
            if (source.Status == 0)
            {
                destination.Status = "جديد";
            }
            else
                destination.Status = "مغلق";
            #endregion
        }
    }
}
