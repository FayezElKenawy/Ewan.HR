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
        [Key]
        public string EmployeeNumber { get; set; }
        [Required]
        public string NationalId { get; set; }
        public string Position { get; set; }
        [Display(Name = "Frist Name")]
        [MaxLength(50)]
        public string FristName { get; set; }
        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DepartementId { get; set; } = "0";
        public string DirectManager { get; set; }
        public string LocationId { get; set; } = "0";
        [Required]
        public DateTime ResumptionDate { get; set; }
        [Required]
        public DateTime ContractStartDate { get; set; }
        [Required]
        public DateTime ContractEndDate { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; } = "0";
        public string Photo { get; set; } = "yyyy";
        public string EmployeeId { get; set; }
        #endregion

        #region ForeignKeys

        [ForeignKey("DepartementId")]
        public Department Departments  { get; set; }

        public ICollection<RequestMasterData> Requests { get; set; }
        #endregion
    }
}
