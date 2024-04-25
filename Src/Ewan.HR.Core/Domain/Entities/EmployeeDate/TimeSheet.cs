using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities
{
    public class TimeSheet : AuditData
    {
        public int EmployeeId { get; set; }

        public EmployeeData Employee { get; set; }
    }
}
