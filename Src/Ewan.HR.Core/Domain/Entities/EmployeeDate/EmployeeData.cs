using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities
{
    public class EmployeeData : AuditData
    {
        public string EmployeeNumber { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? CommencementDate { get; set; } //تاريخ المباشرة
        public bool Gender { get; set; }
        public int RecordId { get; set; }
        public int NatioalityId { get; set; }

        // الراتب
        public decimal BasicSalary { get; set; }
        public decimal HousingAllowance { get; set; }
        public decimal OtherAllowance { get; set; }

        public Record Record { get; set; }
        public Nationality Nationality { get; set; }
    }
}
