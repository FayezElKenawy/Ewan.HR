using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities
{
    public class Nationality : AuditData
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }
}
