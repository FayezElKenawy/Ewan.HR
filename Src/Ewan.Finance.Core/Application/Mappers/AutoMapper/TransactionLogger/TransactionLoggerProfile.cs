using AutoMapper;
using Ewan.Finance.Core.Application.Models.TransactionLogger;
using Ewan.Finance.Core.Domain.Entities;

namespace Ewan.Finance.Core.Application.Mappers.AutoMapper.Receivables
{
    public class TransactionLoggerProfile : Profile
    {
        public TransactionLoggerProfile()
        {
            #region Request
            CreateMap<TransactionLoggerModel, TransactionLogger>().ReverseMap();
            #endregion

            #region Response
          
            #endregion
        }
    }
}
