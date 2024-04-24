using Ewan.HR.Core.Domain.Interfaces;
using Ewan.HR.Core.Domain.Interfaces.Repositories;
using Ewan.HR.InfraStructure.Contexts;
using Ewan.HR.InfraStructure.Repositories;
using SharedCoreLibrary.Domain.Abstractions;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.UinitsOfWork
{
    public class HRUnitOfWork : UnitOfWork<HrContext>, IHRUnitOfWork
    {

        #region Repositories
        public IEmployeeRepository EmployeeRepository { get; private set; }

        #endregion

        public HRUnitOfWork(HrContext context) : base(context)
        {
            EmployeeRepository = new EmployeeRepository(context);
        }

        public override IRepository<TEntity> Repository<TEntity>()
        {
            return new Repository<TEntity, HrContext>(Context);
        }


    }
}
