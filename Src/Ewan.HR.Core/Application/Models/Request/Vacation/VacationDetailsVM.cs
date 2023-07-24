namespace Ewan.HR.Core.Application.Models.Request.Vacation
{
    public class VacationDetailsVM
    {
        public string Number { get; set; }
        public DateTime DateCreated { get; set; }
        public string VacationTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ResumptionDate { get; set; }
        public int TotalDayes { get; set; } = 0;
        public string VacationReason { get; set; }
        public int Balance { get; set; } = 0;
        public int VacationDays { get; set; } = 0;
        public int RemainingBalance { get; set; } = 0;
        public string ReplacingEmployeeNumber { get; set; }
        public string ReplacingEmployeeName { get; set; }
        public byte ReplacingSignature { get; set; }
        public string WorkHandover { get; set; }
        public string JobTitle { get; set; }
        public string ActionFrom { get; set; }
        public string RequestId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorApproval { get; set; }
        public DateTime SupervisorApprovalDate { get; set; }
        public string SDeclineReason { get; set; }
        public string CEOId { get; set; }
        public string CEOName { get; set; }
        public string CEOApproval { get; set; }
        public DateTime CEOApprovalDate { get; set; }
        public string CEODeclineReason { get; set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
        public int RefernceNumber { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Department { get; set; }
        public string EmployeeSignature { get; set; }
        public DateTime SupervisorSentDate { get; set; }
        public byte? SendtoCEO { get; set; }
        public DateTime CEOSentDate { get; set; }
        public byte? SendtoSupervisor { get; set; }
        public string SupervisorApprovalStatus { get; set; }
        public string CEOApprovalStatus { get; set; }
        public string InOutVisaFor { get; set; }
        public string TicketsFor { get; set; }
        public string TicketsVisaCost { get; set; }
        public DateTime LastReturnDate { get; set; }
        public int PaidDays { get; set; }
        public int UnpaidDays { get; set; }
        public int Holidays { get; set; }
        public string LastApproval { get; set; }

    }
}
