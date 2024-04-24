using Ewan.HR.Core.Domain.Interfaces.Repositories;
using SharedCoreLibrary.Domain.Abstractions;

namespace Ewan.HR.Core.Domain.Interfaces
{
    public interface IHRUnitOfWork : IHrUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; }
    }
}
