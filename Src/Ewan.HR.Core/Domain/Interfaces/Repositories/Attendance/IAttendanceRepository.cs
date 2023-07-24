using Ewan.HR.Core.Domain.Entities.Attendance;
using SharedCoreLibrary.Domain.Abstractions;

namespace Ewan.HR.Core.Domain.Interfaces.Repositories.Attendance
{
    public interface IAttendanceRepository:IRepository<EmployeeAttendanceLog>
    {
    }
}
