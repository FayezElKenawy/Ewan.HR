namespace Ewan.HR.Core.Application.Models.Request
{
    public class RequestVM
    {
        public string RequestId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorApproval { get; set; }
        public DateTime SupervisorApprovalDate { get; set; }
        public string SupervisorApprovalStatus { get; set; }
        public string SDeclineReason { get; set; }
        public string CEOId { get; set; }
        public string CEOName { get; set; }
        public string CEOApproval { get; set; }
        public DateTime CEOApprovalDate { get; set; }
        public string CEODeclineReason { get; set; }
        public string CEOApprovalStatus { get; set; }
        public string Status { get; set; }
        public string ApprovalStatusMessage { get; set; }
        public byte ApprovalStatus { get; set; }
        public int RefernceNumber { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Department { get; set; }
        public string EmployeeSignature { get; set; }
        public string DateCreated { get; set; }
        public DateTime SupervisorSentDate { get; set; }
        public byte? SendtoCEO { get; set; }
        public DateTime CEOSentDate { get; set; }
        public byte? SendtoSupervisor { get; set; }
        public string LastApproval { get; set; }

    }
}
