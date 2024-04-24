using SharedCoreLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewan.HR.Core.Domain.Entities
{
    public class TimeSheet : AuditData
    {
        public int EmployeeId { get; set; }

        public EmployeeData Employee { get; set; }
    }
}
