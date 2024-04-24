using Ewan.HR.Core.Domain.Entities;
using Ewan.HR.Core.Domain.Interfaces.Repositories;
using Ewan.HR.InfraStructure.Contexts;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.Repositories
{
    public class EmployeeRepository:Repository<EmployeeData,HrContext>,IEmployeeRepository
    {
        public EmployeeRepository(HrContext hrContext):base(hrContext)
        {
            
        }
    }
}
