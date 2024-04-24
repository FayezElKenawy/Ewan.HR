using Ewan.HR.Core.Domain.Entities.Employee;
using SharedCoreLibrary.Domain.Abstractions;

namespace Ewan.HR.Core.Domain.Interfaces.Repositories.Employee
{
    public interface IEmployeeRepository : IRepository<Entities.Employee.Employee>
    {
    }
}
