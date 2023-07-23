using SharedCoreLibrary.Domain.Abstractions;

namespace SharedCoreLibrary.Domain.Entities
{
    public class AuditData : IAuditData
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string CreatorNameAr { get; set; }
        public string CreatorNameEn { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierNameAr { get; set; }
        public string ModifierNameEn { get; set; }
        public DateTime? ModificationDate { get; set; }
    }

    public class AuditDataWithoutId
    {
        public string CreatorId { get; set; }
        public string CreatorNameAr { get; set; }
        public string CreatorNameEn { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierNameAr { get; set; }
        public string ModifierNameEn { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
