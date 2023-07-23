﻿using Ewan.HR.Core.Domain.Entities.Request.MasterData;
using Ewan.HR.Core.Domain.Interfaces.Repositories.Request;
using Ewan.HR.InfraStructure.Contexts;
using SharedInfraStructureLibrary.Repositories;

namespace Ewan.HR.InfraStructure.Repositories.Request
{
    public class RequestRepository:Repository<RequestMasterData,HrContext>,IRequestRepository
    {
        public RequestRepository(HrContext hrContext) : base(hrContext)
        {
                
        }
    }
}
