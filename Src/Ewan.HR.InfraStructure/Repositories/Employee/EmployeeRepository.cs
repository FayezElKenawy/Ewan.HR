using Ewan.HR.Core.Domain.Entities.Employee;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Employee;
using Ewan.HR.InfraStructure.Contexts;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.Repositories.Employee
{
    public class EmployeeRepository:Repository<EmployeeData,HrContext>,IEmployeeRepository
    {
        public EmployeeRepository(HrContext hrContext):base(hrContext)
        {
            
        }
    }
}
