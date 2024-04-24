using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities
{
    public class TransactionType : AuditData
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public bool IsFullDay  { get; set; }
    }
}
