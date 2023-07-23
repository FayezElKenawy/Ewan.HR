using Ewan.HR.Core.Domain.Entities.Vacation;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Request;
using Ewan.HR.InfraStructure.Contexts;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.Repositories.Request
{
    public class VacationRepository:Repository<VacationRequest,HrContext>,IVacationRepository
    {
        public VacationRepository(HrContext hrContext) : base(hrContext)
        {
                
        }
    }
}
