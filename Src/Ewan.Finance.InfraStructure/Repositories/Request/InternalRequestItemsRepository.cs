using Ewan.HR.Core.Domain.Entities.Request.Internal;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Request;
using Ewan.HR.InfraStructure.Contexts;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.Repositories.Request
{
    public class InternalRequestItemsRepository:Repository<InternalRequestItem,HrContext>,IInternalRequestItemsRepository
    {
        public InternalRequestItemsRepository(HrContext hrContext):base(hrContext)
        {
            
        }
    }
}
