

namespace Ewan.HR.Core.Application.Models.Request
{
    public class AddRequestVM
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public string TypeId { get; set; } = "0";
        public string SupervisorApproval { get; set; } = "0";
        public DateTime SupervisorApprovalDate { get; set; }
        public string CEOApproval { get; set; } = "0";
        public DateTime CEOApprovalDate { get; set; }
        public byte Status { get; set; } = 0;
        public byte ApprovalStatus { get; set; } = 0;
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public string DirectManagerId { get; set; }
        public string DirectManagerName { get; set; }
        public string EmployeeSignature { get; set; }
        public byte? SendtoSupervisor { get; set; } = 1;
        public DateTime SupervisorSentDate { get; set; } = DateTime.Now;
        public byte? SendtoCEO { get; set; }
        public DateTime CEOSentDate { get; set; }
    }
}
