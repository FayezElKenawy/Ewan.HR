using Ewan.HR.Core.Domain.Interfaces;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Attendance;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Employee;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Request;
using Ewan.HR.InfraStructure.Contexts;
using Ewan.HR.InfraStructure.Repositories.Attendance;
using Ewan.HR.InfraStructure.Repositories.Employee;
using Ewan.HR.InfraStructure.Repositories.Request;
using SharedCoreLibrary.Domain.Abstractions;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.UinitsOfWork
{
    public class HRUnitOfWork : UnitOfWork<HrContext>, IHRUnitOfWork
    {
        
        #region Repositories
        public IEmployeeRepository EmployeeRepository { get; private set; }
        public IAttendanceRepository AttendanceData { get; private set; }
        public IRequestRepository RequestRepository { get; private set; }
        public IInternalRequestRepository InternalRequestRepository { get; private set; }
        public IVacationRepository VacationRepository { get; private set; }
        public IInternalRequestItemsRepository InternalRequestItemsRepository { get; private set; }
        public IRequestTypeRepository RequestTypeRepository { get; private set; }
        public IAttendanceRepository AttendanceRepository { get; private set; }

        #endregion

        public HRUnitOfWork(HrContext context) : base(context) 
        {
            EmployeeRepository = new EmployeeRepository(context);
            AttendanceData = new AttendanceRepository(context);
            RequestRepository=new RequestRepository(context);
            InternalRequestRepository = new InternalRequestRepository(context);
            VacationRepository = new VacationRepository(context);
            InternalRequestItemsRepository=new InternalRequestItemsRepository(context);
            RequestTypeRepository = new RequestTypeRepository(context);
            AttendanceRepository = new AttendanceRepository(context);
        }

        public override IRepository<TEntity> Repository<TEntity>()
        {
            return new Repository<TEntity, HrContext>(Context);
        }


    }
}
