using Ewan.HR.Core.Domain.Entities.Attendance;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Attendance;
using Ewan.HR.InfraStructure.Contexts;
using SharedInfraStructureLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewan.HR.InfraStructure.Repositories.Attendance
{
    public class MonthSettingsRepository: Repository<AttendanceMonthSettings, HrContext>, IMonthSettingsRepository
    {
        public MonthSettingsRepository(HrContext context) : base(context)
        {
            
        }
    }
}
