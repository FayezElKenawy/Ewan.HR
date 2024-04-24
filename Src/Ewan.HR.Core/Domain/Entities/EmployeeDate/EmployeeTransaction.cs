using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities
{
    public class EmployeeTransaction : AuditData
    {
        public int EmployeeId { get; set; }
        public int TransactionTypeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal DiscountValue { get; set; }
        public string Notes { get; set; }
        public string AttachmentPath { get; set; }
        public EmployeeData Employee { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
