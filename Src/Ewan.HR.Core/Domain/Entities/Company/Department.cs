using Ewan.HR.Core.Domain.Entities.Employee;
using SharedCoreLibrary.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewan.HR.Core.Domain.Entities.Company
{
    public class Department : AuditData
    {
        #region Fields
        public string DepartmentId { get; set; }
        public string Title { get; set; }
        public string ManagerId { get; set; }
        #endregion

        #region ForeignKeys

        [ForeignKey("ManagerId")]
        public EmployeeData Employee { get; set; }

        #endregion
    }
}
