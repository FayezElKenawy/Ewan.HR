using Ewan.HR.Core.Domain.Entities.Company;
using Ewan.HR.Core.Domain.Entities.Request.MasterData;
using SharedCoreLibrary.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewan.HR.Core.Domain.Entities.Employee
{
    public class EmployeeData:AuditData
    {
        #region Fields
        public string EmployeeNumber { get; set; }
        public int TypeId { get; set; }
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

        #region ForeignKeys

        [ForeignKey("DepartementId")]
        public Department Departments  { get; set; }
        #endregion
    }
}
