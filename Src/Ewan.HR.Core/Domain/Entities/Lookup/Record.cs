using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities
{
    public class Record : AuditData
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

    }
}
