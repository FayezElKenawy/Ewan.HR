using Ewan.HR.Core.Domain.Entities.Vacation;
using SharedCoreLibrary.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ewan.HR.Core.Domain.Entities.Employee;

namespace Ewan.HR.Core.Domain.Entities.Request.MasterData
{
    public class RequestMasterData:AuditDataWithoutId
    {
        #region fields
        [Key]
        public string RequestId { get; set; }
        public string Title { get; set; }
        public string TypeId { get; set; }
        public byte? SendtoSupervisor { get; set; }
        public DateTime SupervisorSentDate { get; set; }
        public string SupervisorId { get; set; }
        public string SupervisorApproval { get; set; }
        public string SupervisorApprovalStatus { get; set; }
        public DateTime SupervisorApprovalDate { get; set; }
        public string SupervisorDeclineReason { get; set; }
        public byte? SendtoCEO { get; set; }
        public DateTime CEOSentDate { get; set; }
        public string CEOId { get; set; }
        public string CEOApproval { get; set; }
        public string CEOApprovalStatus { get; set; }
        public DateTime CEOApprovalDate { get; set; }
        public string CEODeclineReason { get; set; }
        public byte Status { get; set; }
        public byte ApprovalStatus { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RefernceNumber { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Department { get; set; }
        public string EmployeeSignature { get; set; }
        public byte? SendToHr { get; set; }
        public string SendToHrStatus { get; set; }
        public byte HrApproval { get; set; }
        public DateTime SendToHrDat { get; set; }
        public DateTime HrApprovalDate { get; set; }
        public string HrManagerId { get; set; }
        public string HrManagerName { get; set; }
        #endregion

        #region ForeignKeys
        [ForeignKey("TypeId")]
        public RequestType Type { get; set; }
        public VacationRequest Vacation { get; set; }
        [ForeignKey("EmployeeNumber")]
        public Employee.Employee Employee { get; set; }
        #endregion
    }
}
