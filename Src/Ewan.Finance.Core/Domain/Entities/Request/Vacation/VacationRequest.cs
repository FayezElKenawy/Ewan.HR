using Ewan.HR.Core.Domain.Entities.Request.MasterData;
using SharedCoreLibrary.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewan.HR.Core.Domain.Entities.Vacation
{
    public class VacationRequest:AuditDataWithoutId
    {

        [Key]
        public string Number { get; set; }
        public string RequestId { get; set; }
        public string VacationTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastReturnDate { get; set; }
        public int PaidDays { get; set; }
        public int UnpaidDays { get; set; }
        public int Holidays { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ResumptionDate { get; set; }
        public int TotalDayes { get; set; } = 0;
        public string VacationReason { get; set; }
        public int Balance { get; set; } = 0;
        public int VacationDays { get; set; } = 0;
        public int RemainingBalance { get; set; } = 0;
        public byte? SendtoReplacement { get; set; }
        public DateTime SentDate { get; set; }
        public string ReplacingEmployeeNumber { get; set; }
        public string ReplacingEmployeeName { get; set; }
        public byte? ReplacmentApproval { get; set; }
        public string ReplacmentApprovalStatus { get; set; }
        public string RejectReason { get; set; }
        public DateTime ApprovalDate { get; set; }
        public byte ReplacingSignature { get; set; }
        public string WorkHandover { get; set; }
        public string InOutVisaFor { get; set; }
        public string TicketsFor { get; set; }
        public string TicketsVisaCost { get; set; }

        #region Relation
        [ForeignKey("RequestId")]
        public RequestMasterData Request { get; set; }
        #endregion
    }
}
