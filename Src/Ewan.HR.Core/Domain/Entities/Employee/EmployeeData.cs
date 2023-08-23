using Ewan.HR.Core.Domain.Entities.Company;
using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities.Employee
{
    public class EmployeeData:AuditData
    {
        #region Fields
        public string EmployeeNumber { get; set; }
        public int IDType { get; set; }
        public string NationalId { get; set; }
        public string Position { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DepartementId { get; set; }
        public string DirectManager { get; set; }
        public string LocationId { get; set; }
        public DateTime ResumptionDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        #endregion

        
        public Department Department  { get; set; }

    }
}
