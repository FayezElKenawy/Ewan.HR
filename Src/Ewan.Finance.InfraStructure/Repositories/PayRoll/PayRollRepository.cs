using Ewan.HR.Core.Domain.Entities.PayRoll;
using Ewan.HR.Core.Domain.Interfaces.Repositories.PayRoll;
using Ewan.HR.InfraStructure.Contexts;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.Repositories.PayRoll
{
    public class PayRollRepository:Repository<PayRollData,HrContext>,IPayRollRepository
    {
        public PayRollRepository(HrContext hrContext):base(hrContext)
        {
            
        }
    }
}
