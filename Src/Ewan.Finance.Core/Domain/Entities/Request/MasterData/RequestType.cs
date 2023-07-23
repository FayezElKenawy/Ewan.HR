using Ewan.HR.Core.Domain.Entities.Company;
using SharedCoreLibrary.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewan.HR.Core.Domain.Entities.Request.MasterData
{
    public class RequestType :AuditDataWithoutId
    {
        #region Fields
        [Key]
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string DepartmentId { get; set; }
        #endregion

        #region Navigations
        public ICollection<RequestMasterData> Requests { get; set; }
        #endregion

        #region Foreign Keys
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        #endregion
    }
}
