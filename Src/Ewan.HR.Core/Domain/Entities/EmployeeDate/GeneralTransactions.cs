using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities
{
    public class GeneralTransaction : AuditData
    {
        public string Name { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
