using Ewan.HR.Core.Domain.Interfaces.Repositories.Attendance;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Employee;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Request;
using SharedCoreLibrary.Domain.Abstractions;

namespace Ewan.HR.Core.Domain.Interfaces
{
    public interface IHRUnitOfWork: IUnitOfWork
    {
        public IRequestRepository RequestRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IInternalRequestRepository InternalRequestRepository { get; }
        public IVacationRepository VacationRepository { get; }
        public IInternalRequestItemsRepository InternalRequestItemsRepository { get; }
        public IRequestTypeRepository RequestTypeRepository { get; }
        public IAttendanceRepository AttendanceRepository { get; }
    }
}
