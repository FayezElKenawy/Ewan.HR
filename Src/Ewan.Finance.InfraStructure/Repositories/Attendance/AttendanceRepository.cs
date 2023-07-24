using Ewan.HR.Core.Domain.Entities.Attendance;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Attendance;
using Ewan.HR.InfraStructure.Contexts;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.Repositories.Attendance
{
    public class AttendanceRepository:Repository<EmployeeAttendanceLog,HrContext>,IAttendanceRepository
    {
        public AttendanceRepository(HrContext context):base(context)
        {
                
        }
    }
}
