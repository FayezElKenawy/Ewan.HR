using Ewan.HR.Core.Domain.Entities.Attendance;
using SharedCoreLibrary.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewan.HR.Core.Domain.Interfaces.Repositories.Attendance
{
    public interface IMonthSettingsRepository: IRepository<AttendanceMonthSettings>
    {
    }
}
