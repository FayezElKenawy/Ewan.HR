using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities.Company
{
    public class Department : AuditData
    {
        #region Fields
        public string Title { get; set; }
        #endregion
    }
}
