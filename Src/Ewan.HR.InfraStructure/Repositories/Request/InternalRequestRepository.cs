using Ewan.HR.Core.Domain.Entities.Request.Internal;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Request;
using Ewan.HR.InfraStructure.Contexts;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.Repositories.Request
{
    public class InternalRequestRepository:Repository<InternalRequest,HrContext>,IInternalRequestRepository
    {
        public InternalRequestRepository(HrContext hrContext):base(hrContext)
        {
            
        }
    }
}
