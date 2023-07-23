namespace SharedCoreLibrary.Domain.Abstractions
{
    public interface IAuditData
    {
        public int Id { get; set; }
        string CreatorId { get; set; }
        string CreatorNameAr { get; set; }
        string CreatorNameEn{ get; set; }
        DateTime? CreationDate { get; set; }
        string ModifierId { get; set; }
        string ModifierNameAr { get; set; }
        string ModifierNameEn { get; set; }
        DateTime? ModificationDate { get; set; }
    }
}
